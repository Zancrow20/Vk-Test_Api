using DtoLibrary;
using Microsoft.EntityFrameworkCore;
using VkWebApi.DataAccess.Models;
using VkWebApi.DataAccess.VkDbContext;

namespace VkWebApi.Services.UserService;

public class UserService : IUserService
{
    private readonly VkDbContext _dbContext;

    public UserService(VkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User?>> GetAll(int page, int entitiesForPage)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u!.UserGroup)
            .Include(u => u!.UserState)
            .Skip((page - 1) * entitiesForPage)
            .Take(entitiesForPage)
            .ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _dbContext.Users
            .Where(u => u.Id == id)
            .Include(u => u.UserGroup)
            .Include(u => u!.UserState)
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id && u.UserState.State == Status.Active);

        if (user == null)
            return false;
        user.UserStateId = 2;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateUser(UserDto dto)
    {
        var existingUser = await _dbContext.Users.AnyAsync(u => dto.Login == u.Login && u.UserState.State == Status.Active);

        if (existingUser)
            return false;
        
        if (dto.Role == Role.Admin && await _dbContext.Users.AnyAsync(u => u.UserGroup.Role == Role.Admin))
            return false;
        
        var user = new User()
        {
            Created_Date = DateTime.Now,
            Login = dto.Login,
            Password = dto.Password,
            UserGroupId = (int)dto.Role,
            UserStateId = 1
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        await Task.Delay(5000);
        return true;
    }
}
using DtoLibrary;
using Microsoft.EntityFrameworkCore;
using VkWebApi.DataAccess.Models;
using VkWebApi.DataAccess.VkDbContext;

namespace VkWebApi.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly VkDbContext _dbContext;


    public AuthService(VkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RegistrationDto> Register(UserDto user)
    {
        if (await UserExists(user.Login))
            return new RegistrationDto("User already exists!");
        if (await CheckAdminRole(user))
            return new RegistrationDto("Admin already exists!");
        await _dbContext.Users.AddAsync(new User()
        {
            Login = user.Login,
            Password = user.Password,
            UserGroupId = (int) user.Role,
            UserStateId = 1,
            Created_Date = DateTime.Now
        });
        await _dbContext.SaveChangesAsync();
        await Task.Delay(5000);
        return new RegistrationDto();
    }

    private async Task<bool> CheckAdminRole(UserDto user)
    {
        if (user.Role != Role.Admin) return false;
        return await _dbContext.Users.AnyAsync(
            u => u.UserGroup.Role == Role.Admin && u.UserState.State == Status.Active);
    }

    public async Task<User> Authenticate(string login, string password)
    {
        return await _dbContext.Users
            .SingleOrDefaultAsync(u => u.Login == login && u.Password == password);
    }
    

    private async Task<bool> UserExists(string login)
    {
        return await _dbContext.Users.AnyAsync(u => u.Login == login && u.UserState.State == Status.Active);
    }
}
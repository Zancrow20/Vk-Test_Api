using DtoLibrary;
using VkWebApi.DataAccess.Models;

namespace VkWebApi.Services.UserService;

public interface IUserService
{
    Task<List<User?>> GetAll(int page, int entitiesForPage);
    Task<User?> GetUserById(int id);
    Task<bool> DeleteUser(int id);
    Task<bool> CreateUser(UserDto user);
}
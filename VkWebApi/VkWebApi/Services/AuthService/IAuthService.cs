using DtoLibrary;
using VkWebApi.DataAccess.Models;

namespace VkWebApi.Services.AuthService;

public interface IAuthService
{
    Task<RegistrationDto> Register(UserDto user);

    Task<User> Authenticate(string login, string password);
}
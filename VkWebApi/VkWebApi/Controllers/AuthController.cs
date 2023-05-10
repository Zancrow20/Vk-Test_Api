using DtoLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using VkWebApi.Services.AuthService;

namespace VkWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        var registerResult = await _authService.Register(user);
        if (registerResult.IsSuccessful)
        {
            return Ok("User successfully created");
        }
        return BadRequest(registerResult.ErrorMessage);
    }
}
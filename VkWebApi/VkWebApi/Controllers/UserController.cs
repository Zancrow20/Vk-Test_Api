using DtoLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkWebApi.Services.UserService;

namespace VkWebApi.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("users")]
    public async Task<IActionResult> GetAllUsers([FromBody] GetUsersDto dto)
    {
        if (dto.Page < 1) dto.Page = 1;
        if (dto.PageSize < 1) dto.PageSize = 16;
        var users = await _userService.GetAll(dto.Page, dto.PageSize);
        return Ok(users);
    }

    [HttpPost("user")]
    public async Task<IActionResult> GetUserById([FromBody] GetUserByIdDto dto)
    {
        var user = await _userService.GetUserById(dto.Id);
        if (user == null)
            return NotFound("User doesn't exist");
        return Ok(user);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto user)
    {
        var creatingResult = await _userService.CreateUser(user);
        if (creatingResult)
            return Ok("User successfully created");
        return BadRequest("User already exists");
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteUser([FromBody]DeleteUserDto dto)
    {
        var deletingResult = await _userService.DeleteUser(dto.Id);
        if (deletingResult)
            return Ok("User successfully deleted");
        return BadRequest("User doesn't exist");
    }
}
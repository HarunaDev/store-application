using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Services;
using StoreApp.DTOs.Responses;
using StoreApp.DTOs.User;


namespace StoreApp.Controllers;

[Authorize]
[ApiController]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult>
        GetUsers()
    {
        var users = await _userService.GetUsersAsync();

        return Ok(new ApiResponse<IEnumerable<UserResponseDto>>
        {
            Success = true,
            Message = "Users retreived successfully",
            Data = users
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        return Ok(new ApiResponse<UserResponseDto>
        {
            Success = true,
            Message = "User retreived successfully",
            Data = user
        });
    }
}
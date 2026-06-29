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
    [ProducesResponseType(typeof(ApiResponse<UserPagedResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, meta) = await _userService.GetUsersAsync(pageNumber, pageSize);
        // var users = await _userService.GetUsersAsync();

        var userDtos = items.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email
        });

        var response = new UserPagedResponse
        {
            PageNumber = meta.PageNumber,
            PageSize = meta.PageSize,
            TotalRecords = meta.TotalRecords,
            Users = userDtos
        };

        return Ok(new ApiResponse<UserPagedResponse>
        {
            Success = true,
            Message = "Users retreived successfully",
            Data = response
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
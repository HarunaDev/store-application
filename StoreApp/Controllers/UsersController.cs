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
        try
        {
            var users = await _userService.GetUsersAsync();

            if (users is null)
            {
                return NotFound();
            }
            var userDtos = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            });

            return Ok(new ApiResponse<IEnumerable<UserResponseDto>>
            {
                Success = true,
                Message = "Users retreived successfully",
                Data = userDtos
            });
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "FETCH_USERS_FAILED",
                Message = "Unable to fetch users"
            });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser(string id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            var userDto = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return Ok(new ApiResponse<UserResponseDto>
            {
                Success = true,
                Message = "User retreived successfully",
                Data = userDto
            });
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "FETCH_USER_FAILED",
                Message = "Unable to fetch user"
            });
        }
    }
}
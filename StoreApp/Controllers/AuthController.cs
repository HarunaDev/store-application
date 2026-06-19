using Microsoft.AspNetCore.Mvc;
using StoreApp.DTOs;
using StoreApp.Services;
using StoreApp.DTOs.Responses;
using StoreApp.DTOs.Auth;

namespace StoreApp.Controllers;

[ApiController]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(
        AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto)
    {
        try
        {
            await _authService.RegisterAsync(dto);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User registered successfully"
            });
        }
        catch (Exception)
        {
            // log ex internally
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "REGISTRATION_FAILED",
                Message = "Unable to register user"
            });
        }
    }

    [ProducesResponseType(typeof(ApiResponse<AuthResultDto>), StatusCodes.Status200OK)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponse<AuthResultDto>
            {
                Success = true,
                Message = "User Logged in successfully",
                Data = result
            });
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "AUTHENTICATION_FAILED",
                Message = "Unable to authenticate user"
            });
        }
    }

    [ProducesResponseType(typeof(ApiResponse<TokenDto>), StatusCodes.Status200OK)]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto dto)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponse<TokenDto>
            {
                Success = true,
                Message = "Success",
                Data = result
            });
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "AUTHENTICATION_FAILED",
                Message = "Unable to get tokens"
            });
        }
    }
}
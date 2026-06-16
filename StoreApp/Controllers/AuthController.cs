using Microsoft.AspNetCore.Mvc;
using StoreApp.DTOs;
using StoreApp.Services;

namespace StoreApp.Controllers;

[ApiController]
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
        await _authService.RegisterAsync(dto);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        // var token =
        //     _authService.Login(dto);

        // return Ok(new
        // {
        //     accessToken = token
        // });
        var result =
        await _authService.LoginAsync(dto);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto);

        return Ok(result);
    }
}
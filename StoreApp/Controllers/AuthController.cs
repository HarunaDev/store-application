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
    public IActionResult Register(
        RegisterDto dto)
    {
        _authService.Register(dto);

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(
        LoginDto dto)
    {
        var token =
            _authService.Login(dto);

        return Ok(new
        {
            accessToken = token
        });
    }
}
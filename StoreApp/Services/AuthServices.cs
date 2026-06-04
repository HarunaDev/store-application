using StoreApp.DTOs;
using StoreApp.Models;

namespace StoreApp.Services;

public class AuthService
{
    private readonly List<User> _users = [];

    private readonly PasswordService _passwordService;

    private readonly TokenService _tokenService;

    public AuthService(
        PasswordService passwordService,
        TokenService tokenService)
    {
        _passwordService =
            passwordService;

        _tokenService =
            tokenService;
    }

    public void Register(RegisterDto dto)
    {
        if (_users.Any(
    u =>
        u.Email.Equals(
            dto.Email,
            StringComparison.OrdinalIgnoreCase)
        ||
        u.UserName.Equals(
            dto.UserName,
            StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception(
                "Username or Email already exists");
        }

        _users.Add(new User
        {
            Id = _users.Count + 1,
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash =
                _passwordService.Hash(
                    dto.Password)
        });
    }

    public string Login(LoginDto dto)
    {
        var user = _users.FirstOrDefault(
            u => u.Email == dto.Email);

        if (user is null)
        {
            throw new Exception(
                "Invalid credentials");
        }

        var valid =
            _passwordService.Verify(
                user.PasswordHash,
                dto.Password);

        if (!valid)
        {
            throw new Exception(
                "Invalid credentials");
        }

        return _tokenService
            .GenerateAccessToken(
                user.Email);
    }
}
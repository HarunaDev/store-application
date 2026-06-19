using StoreApp.DTOs;
using StoreApp.DTOs.Auth;
using StoreApp.Models;
using StoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Services;

public class AuthService
{
    // private readonly List<User> _users = [];

    private readonly PasswordService _passwordService;

    private readonly TokenService _tokenService;

    private readonly StoreAppDbContext _context;

    public AuthService(
        StoreAppDbContext context,
        PasswordService passwordService,
        TokenService tokenService)
    {
        _context = context;
        _passwordService =
            passwordService;

        _tokenService =
            tokenService;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {

        var exists =
        await _context.Users.AnyAsync(
            u =>
                u.Email == dto.Email ||
                u.UserName == dto.UserName);

        if (exists)
        {
            throw new Exception(
                "Username or Email already exists");
        }

        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash =
                _passwordService.Hash(
                    dto.Password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task<AuthResultDto> LoginAsync(
    LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == dto.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
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

        var accessToken =
            _tokenService.GenerateAccessToken(
                user.Id,
                user.Email,
                user.UserName);

        var refreshToken =
            _tokenService.GenerateRefreshToken();

        _context.RefreshTokens.Add(
            new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt =
                    DateTime.UtcNow.AddDays(7)
            });

        await _context.SaveChangesAsync();

        return new AuthResultDto
        {
            UserId = user.Id,
            Tokens = new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }

    public async Task<TokenDto> RefreshTokenAsync(
        RefreshTokenRequestDto dto)
    {
        var token =
            await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(
                    r => r.Token ==
                         dto.RefreshToken);

        if (token is null)
        {
            throw new Exception(
                "Invalid refresh token");
        }

        if (token.IsRevoked)
        {
            throw new Exception(
                "Refresh token revoked");
        }

        if (token.ExpiresAt < DateTime.UtcNow)
        {
            throw new Exception(
                "Refresh token expired");
        }

        var accessToken =
            _tokenService.GenerateAccessToken(
                token.User.Id,
                token.User.Email,
                token.User.UserName);

        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = token.Token
        };
    }
}
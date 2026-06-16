using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoreApp.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(
        int userId, string email, string userName)
    {
        var claims = new List<Claim>
        {
            // new Claim(
            //     ClaimTypes.Email,
            //     email)
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.UniqueName, userName),

            // Optional but useful
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, userName)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:SecretKey"]!));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],
                audience:
                    _configuration["Jwt:Audience"],
                claims: claims,
                expires:
                    DateTime.UtcNow.AddMinutes(
                        15),
                signingCredentials:
                    creds);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(bytes);
    }
}
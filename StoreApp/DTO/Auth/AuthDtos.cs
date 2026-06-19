namespace StoreApp.DTOs.Auth;

public class AuthResultDto
{
    public string UserId { get; set; } = "";
    public required TokenDto Tokens { get; set; }
}

public class TokenDto
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}

public class LoginDto
{
    public string Email { get; set; } = "";
    public required string Password { get; set; }
}

public class RegisterDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; } 
}

public class RefreshTokenRequestDto
{
    public required string RefreshToken { get; set; }
}
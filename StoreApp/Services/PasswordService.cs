using Microsoft.AspNetCore.Identity;

namespace StoreApp.Services;

public class PasswordService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string Hash(string password)
    {
        return _hasher.HashPassword("", password);
    }

    public bool Verify(
        string hash,
        string password)
    {
        return _hasher.VerifyHashedPassword(
            "",
            hash,
            password)
            != PasswordVerificationResult.Failed;
    }
}
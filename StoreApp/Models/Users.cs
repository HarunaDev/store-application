namespace StoreApp.Models;

public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public bool EmailVerified { get; set; }
}
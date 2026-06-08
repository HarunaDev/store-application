// using System.ComponentModel.DataAnnotations;
// using Swashbuckle.AspNetCore.Annotations;
namespace StoreApp.DTOs;

public class RegisterDto
{
    // [Required]
    // [RegularExpression(
    //     @"^[a-zA-Z0-9_]+$",
    //     ErrorMessage =
    //     "Username contains invalid characters")]
    public string UserName { get; set; } = "";

    // [Required]
    // [EmailAddress]
    public string Email { get; set; } = "";

    // [Required]
    // [MinLength(8)]
    public string Password { get; set; } = "";
}
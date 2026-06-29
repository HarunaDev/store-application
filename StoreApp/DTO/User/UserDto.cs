using System.ComponentModel.DataAnnotations;
namespace StoreApp.DTOs.User;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
}

public class UserRequestDto
{
    [Required(ErrorMessage = "User id is required")]
    public required string Id { get; set; }
}

public class UserResponseDto
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
}
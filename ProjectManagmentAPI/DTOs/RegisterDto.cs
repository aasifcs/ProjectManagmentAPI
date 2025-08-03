using ProjectManagmentAPI.Models;

namespace ProjectManagmentAPI.DTOs
{
    public class RegisterDto { public string Email { get; set; } public string Username { get; set; } public string Password { get; set; } public UserRole Role { get; set; } }

}

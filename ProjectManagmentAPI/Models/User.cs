using System.ComponentModel.DataAnnotations;

namespace ProjectManagmentAPI.Models
{
    public enum UserRole { Admin, ProjectManager, Developer, Viewer }

    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

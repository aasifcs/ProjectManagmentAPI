using System.ComponentModel.DataAnnotations;

namespace ProjectManagmentAPI.Models
{
    public enum ProjectStatus { NotStarted, InProgress, Completed }

    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ProjectManagerId { get; set; }

        public string DeveloperIds { get; set; } // comma-separated user IDs

        public DateTime CreatedAt { get; set; }

        [Required]
        public ProjectStatus Status { get; set; }
    }
}

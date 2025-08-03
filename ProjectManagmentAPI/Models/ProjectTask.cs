using System.ComponentModel.DataAnnotations;

namespace ProjectManagmentAPI.Models
{
    public enum TaskStatus { Pending, InProgress, Done }

    public class ProjectTask
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int AssignedToId { get; set; }

        [Required]
        public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DueDate { get; set; }
    }
}

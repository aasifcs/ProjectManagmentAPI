namespace ProjectManagmentAPI.Models
{
    public enum ProjectStatus { NotStarted, InProgress, Completed }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectManagerId { get; set; }
        public string DeveloperIds { get; set; } 
        public DateTime CreatedAt { get; set; }
        public ProjectStatus Status { get; set; }
    }
}

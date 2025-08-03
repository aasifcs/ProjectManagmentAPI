using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.Models;

namespace ProjectManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context) => _context = context;

        [Authorize]
        [HttpGet("project/{projectId}")]
        public IActionResult GetByProject(int projectId) => Ok(
            _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToList());

        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        [HttpPost("project/{projectId}")]
        public IActionResult Create(int projectId, ProjectTask task)
        {
            task.ProjectId = projectId;
            task.CreatedAt = DateTime.UtcNow;
            _context.ProjectTasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProjectTask updated)
        {
            var task = _context.ProjectTasks.Find(id);
            if (task == null) return NotFound();
            task.Title = updated.Title;
            task.Description = updated.Description;
            task.Status = updated.Status;
            task.DueDate = updated.DueDate;
            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _context.ProjectTasks.Find(id);
            if (task == null) return NotFound();
            _context.ProjectTasks.Remove(task);
            _context.SaveChanges();
            return Ok();
        }
    }
}

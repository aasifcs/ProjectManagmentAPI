using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.Models;

namespace ProjectManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context) => _context = context;

        [Authorize]
        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Projects.ToList());

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPost]
        public IActionResult Create(Project project)
        {
            project.CreatedAt = DateTime.UtcNow;
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Ok(project);
        }

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Project updated)
        {
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();
            project.Name = updated.Name;
            project.Description = updated.Description;
            project.Status = updated.Status;
            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_context.Projects.Find(id));
    }
}


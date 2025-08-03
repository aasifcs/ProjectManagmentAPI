using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Get all projects")]
        [ProducesResponseType(typeof(IEnumerable<Project>), 200)]
        public IActionResult GetAll() => Ok(_context.Projects.ToList());

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new project")]
        [ProducesResponseType(typeof(Project), 200)] 
        public IActionResult Create(Project project)
        {
            project.CreatedAt = DateTime.UtcNow;
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Ok(project);
        }

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a project by ID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        [SwaggerOperation(Summary = "Delete a project by ID (Admin only)")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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


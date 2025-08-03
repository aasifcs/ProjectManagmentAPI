using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.DTOs;
using ProjectManagmentAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace ProjectManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context) => _context = context;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all users (Admin only)")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public IActionResult GetUsers() => Ok(_context.Users.ToList());

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        [SwaggerOperation(Summary = "Update user role by ID (Admin only)")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(int id, [FromBody] UpdateRoleDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            user.Role = dto.Role;
            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpGet("profile")]
        [SwaggerOperation(Summary = "Get profile of currently logged-in user")]
        [ProducesResponseType(typeof(User), 200)]
        public IActionResult Profile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _context.Users.Find(userId);
            return Ok(user);
        }
    }
}

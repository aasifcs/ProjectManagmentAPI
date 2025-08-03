using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.DTOs;
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
        public IActionResult GetUsers() => Ok(_context.Users.ToList());

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
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
        public IActionResult Profile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _context.Users.Find(userId);
            return Ok(user);
        }
    }
}

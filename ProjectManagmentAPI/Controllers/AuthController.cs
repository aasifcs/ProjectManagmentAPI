using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.DTOs;
using ProjectManagmentAPI.Models;
using ProjectManagmentAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(ApplicationDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login user", Description = "Validates user credentials and returns a JWT token.")]
        [SwaggerResponse(200, "JWT token returned")]
        [SwaggerResponse(401, "Invalid credentials")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register a new user (Admin only)")]
        [SwaggerResponse(200, "User registered successfully")]
        [SwaggerResponse(400, "User already exists")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("User already exists");

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}

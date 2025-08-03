using ProjectManagmentAPI.Data;
using ProjectManagmentAPI.Models;

namespace ProjectManagmentAPI.Seed
{
    public static class DbSeeder
    {
        public static void SeedUsers(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Users.Any())
            {
                var users = new List<User>
            {
                new User { Username = "admin", Email = "admin@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), Role = UserRole.Admin, CreatedAt = DateTime.UtcNow },
                new User { Username = "pm", Email = "pm@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("PM123!"), Role = UserRole.ProjectManager, CreatedAt = DateTime.UtcNow },
                new User { Username = "dev", Email = "dev@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Dev123!"), Role = UserRole.Developer, CreatedAt = DateTime.UtcNow },
                new User { Username = "viewer", Email = "viewer@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Viewer123!"), Role = UserRole.Viewer, CreatedAt = DateTime.UtcNow }
            };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}

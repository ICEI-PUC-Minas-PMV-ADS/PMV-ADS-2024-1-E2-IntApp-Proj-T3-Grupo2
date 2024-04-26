using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Padrinly.Domain.Entities;

namespace Padrinly.Data
{
    public class ApplicationDbSeed
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbSeed(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            if (_context.Users.Any())
                return;

            var pwd = "123@mudar";
            var passwordHasher = new PasswordHasher<User>();

            var adminRole = new Role("Admin");
            adminRole.NormalizedName = adminRole.Name!.ToUpper();

            var institutionRole = new Role("Institution");
            institutionRole.NormalizedName = institutionRole.Name!.ToUpper();

            var patronRole = new Role("Patron");
            patronRole.NormalizedName = patronRole.Name!.ToUpper();

            var studentRole = new Role("Student");
            studentRole.NormalizedName = studentRole.Name!.ToUpper();

            var responsibleRole = new Role("Responsible");
            responsibleRole.NormalizedName = responsibleRole.Name!.ToUpper();

            var roles = new List<Role>()
            {
                adminRole,
                institutionRole,
                patronRole,
                studentRole,
                responsibleRole
            };

            _context.Roles.AddRange(roles);
            _context.SaveChanges();

            var adminUser = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

            var users = new List<User>()
            {
                adminUser
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();

            var userRoles = new List<IdentityUserRole<int>>
            {
                new()
                {
                    UserId = users[0].Id,
                    RoleId = roles.First(q => q.Name == "Admin").Id
                }
            };

            _context.UserRoles.AddRange(userRoles);
            _context.SaveChanges();
        }
    }

    public static class ApplicationExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app, bool isDevelopment)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                if (!isDevelopment)
                    context.Database.Migrate();
                var seeder = new ApplicationDbSeed(context);
                seeder.Seed();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred initializing the DB.");
            }
        }
    }

}

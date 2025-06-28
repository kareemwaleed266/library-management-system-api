using Library.Data.Context;
using Library.Data.Entites.IdentityEntities;
using Library.Repository.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<LibraryManagementSystemIdentityDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRoles>>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await AppIdentitySeeding.SeedRolesAsync(roleManager, loggerFactory);
                    await AppIdentitySeeding.SeedAdminAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}

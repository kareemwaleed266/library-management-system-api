using Library.Data.Entites.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Seeding
{
    public static class AppIdentitySeeding
    {
        public static async Task SeedRolesAsync(RoleManager<AppRoles> roleManager, ILoggerFactory loggerFactory)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new[]
                {
                    new AppRoles
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "Admin"
                    },

                    new AppRoles
                    {
                        Name = "Customer",
                        NormalizedName = "CUSTOMER",
                        ConcurrencyStamp = "Customer"
                    },

                    new AppRoles
                    {
                        Name = "Publisher",
                        NormalizedName = "PUBLISHER",
                        ConcurrencyStamp = "Publisher"
                    }
                };

                foreach (var role in roles)
                {
                    var result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        var loggerFectory = loggerFactory.CreateLogger($"Failed to create role {role.Name}");
                    }
                }

            }
        }
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                Email = "kareemwaleed654@gmail.com",
                NormalizedEmail = "kareemwaleed654@gmail.com".ToUpper(),
                UserName = "kareemwaleed",
                NormalizedUserName = "kareemwaleed".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                IsAdmin = true,
                UserRoles = (UserRoles)Roles.Admin
            };

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.CreateAsync(user, "#Adminuser@12345");
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

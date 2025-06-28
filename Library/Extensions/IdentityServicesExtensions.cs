using Library.Data.Context;
using Library.Data.Entites.IdentityEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.Api.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _configuration)
        {
            var builder = services.AddIdentity<AppUser, AppRoles>()
            .AddEntityFrameworkStores<LibraryManagementSystemIdentityDbContext>()
            .AddDefaultTokenProviders();
            //var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);


            builder.AddEntityFrameworkStores<LibraryManagementSystemIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(
                                               Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                 ValidateIssuer = true,
                 ValidIssuer = _configuration["Token:Issuer"],
                 ValidateAudience = true,
                 ValidAudience = _configuration["Token:Issuer"],
                 ValidateLifetime = true,
                 ClockSkew = TimeSpan.Zero
             };
         });
            return services;
        }
    }
}

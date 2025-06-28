
using Library.Api.Extensions;
using Library.Api.Helper;
using Library.API.Extensions;
using Library.API.MiddleWares;
using Library.Data.Context;
using Library.Service.HandleResponses;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Library
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddDbContext<LibraryManagementSystemDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<LibraryManagementSystemIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            //builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<LibraryManagementSystemDbContext>();

            builder.Services.AddAppServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<EncryptionHelper>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                         .WithOrigins("https://kareemwaleed266.github.io")
                         .AllowCredentials()

                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

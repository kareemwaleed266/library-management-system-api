

using Library.Data.Context;
using Library.Repository.Interfaces;
using Library.Repository.Repositories;
using Library.Service.BookService;
using Library.Service.BookService.Dtos;
using Library.Service.HandleResponses;
using Library.Service.TokenService;
using Library.Service.TransactionService;
using Library.Service.TransactionService.Dtos;
using Library.Service.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(provider =>
            {
                var context = provider.GetRequiredService<LibraryManagementSystemDbContext>();
                var identityContext = provider.GetRequiredService<LibraryManagementSystemIdentityDbContext>();

                // Custom logic to decide which context to use
                return new UnitOfWork(context, identityContext);
            });
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddAutoMapper(typeof(TransactionProfile));
            services.AddAutoMapper(typeof(BookProfile));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(model => model.Value.Errors.Count > 0)
                    .SelectMany(model => model.Value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}

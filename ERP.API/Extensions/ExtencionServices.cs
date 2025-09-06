using ERP.Application.Core.Auth.Commands.Authentication;
using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Commands.Users;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Application.Core.Auth.Queries.Users;
using ERP.Application.Mappings;
using ERP.Domain.Entities;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using ERP.Infrastructure.Repositories;

namespace ERP.API.Extensions
{
    public static class ExtencionServices
    {
        public static IServiceCollection AddApiErpExtention(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(UserProfile), typeof(AuthProfile));

            // Authentication Commands
            services.AddScoped<ILoginCommand, LoginCommand>();

            // User Commands
            services.AddScoped<CreateUser>();
            services.AddScoped<UpdateUser>();
            services.AddScoped<DeleteUser>();
            services.AddScoped<IUserCommandHandler, UserCommandHandler>();

            // User Queries  
            services.AddScoped<GetUserById>();
            services.AddScoped<GetAllUsers>();
            services.AddScoped<IUserQueryHandler, UserQueryHandler>();

            // Repositories
            services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
            services.AddScoped<IRepositoryBase<Session>, RepositoryBase<Session>>();

            return services;
        }
    }
}

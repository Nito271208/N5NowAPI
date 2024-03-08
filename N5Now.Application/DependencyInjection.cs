using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Now.Domain.Entities;
using Nest;
using System.Reflection;

namespace N5Now.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //Injeccion del MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton(configuration);

            //Injeccion del Fluent Validation
            services.AddFluentValidation(option =>
            {
                option.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });

            services.AddScoped<Permission>();
            services.AddScoped<PermissionType>();

            return services;
        }
    }       
}

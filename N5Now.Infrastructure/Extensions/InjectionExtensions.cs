using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Now.Domain.Entities;
using N5Now.Infrastructure.Commons;
using N5Now.Infrastructure.Persistences.Contexts;
using Nest;

namespace N5Now.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DbApiContext).Assembly.FullName;
            //Injeccion del DbApiContext
            services.AddDbContext<DbApiContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DBApiConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            //Injeccion del ElasticSearch
            services.AddSingleton<IElasticClient>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var elasticsearchServerUrl = configuration["Elasticsearch:ServerUrl"];
                var defaultIndex = configuration["Elasticsearch:DefaultIndex"];

                var settings = new ConnectionSettings(new Uri(elasticsearchServerUrl!))
                    .DefaultIndex(defaultIndex);

                return new ElasticClient(settings);
            });

            services.AddScoped<CommonsPermission>();
            services.AddTransient<Permission>();
            services.AddTransient<PermissionType>();

            return services;
        }
    }
}

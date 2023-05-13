#nullable disable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Catalog.Core.Application.Extensions;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Infrastructure.DbContexts;
using SM.Catalog.Infrastructure.Repositories;
using SM.Resource.Data;
using SM.Resource.Messagens.CommonMessage.Notifications;

namespace SM.Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationLayer(configuration);
            services.AddDatabasePersistence(configuration);

            // Repositories
            services.AddRepositories();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Context
            services.AddScoped<CatalogDbContext>();
            services.AddScoped<IUnitOfWork, CatalogDbContext>();
        }

        private static void AddDatabasePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseMySQL(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)
                ),
                ServiceLifetime.Scoped);
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}

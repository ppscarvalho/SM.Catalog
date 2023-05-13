#nullable disable

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Catalog.Core.Application.AutoMappings;
using SM.Catalog.Core.Application.Commands.Category;
using SM.Catalog.Core.Application.Consumers;
using SM.Catalog.Core.Application.Handlers;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Category;
using SM.MQ.Configuration;
using SM.MQ.Extensions;
using SM.MQ.Models;
using SM.Resource.Communication.Mediator;
using SM.Resource.Util;
using System.Reflection;
using IPublisher = SM.MQ.Configuration.IPublisher;

namespace SM.Catalog.Core.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapping
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()), typeof(object));

            // Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Query
            services.AddScoped<IRequestHandler<GetCategoryByIdQuery, CategoryModel>, CategoryQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryModel>>, CategoryQueryHandler>();

            // Command
            services.AddScoped<IRequestHandler<AddCategoryCommand, DefaultResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand, DefaultResult>, CategoryCommandHandler>();

            // RabbitMQ
            services.AddRabbitMq(configuration);
        }

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new BuilderBus(configuration["RabbitMq:ConnectionString"])
            {
                Consumers = new HashSet<Consumer>
                {
                    new Consumer(
                        queue: configuration["RabbitMq:ConsumerCategory"],
                        typeConsumer: typeof(RPCConsumerCategory),
                        quorumQueue: true
                    )
                },

                Publishers = new HashSet<IPublisher>
                {
                    new Publisher<RequestIn>(queue: configuration["RabbitMq:ConsumerCategory"])
                },

                Retry = new Retry(retryCount: 3, interval: TimeSpan.FromSeconds(60))
            };
            services.AddEventBus(builder);
        }
    }
}

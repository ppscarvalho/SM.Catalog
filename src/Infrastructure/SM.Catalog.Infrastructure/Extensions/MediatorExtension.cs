using SM.Catalog.Infrastructure.DbContexts;
using SM.Resource.Communication.Mediator;
using SM.Resource.Domain;

namespace SM.Catalog.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvent(this IMediatorHandler mediator, CatalogDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}

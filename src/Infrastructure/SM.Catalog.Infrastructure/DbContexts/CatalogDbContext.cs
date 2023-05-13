using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SM.Catalog.Core.Domain.Entities;
using SM.Catalog.Infrastructure.Extensions;
using SM.Resource.Communication.Mediator;
using SM.Resource.Data;
using SM.Resource.Domain;
using SM.Resource.Messagens;

namespace SM.Catalog.Infrastructure.DbContexts
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediatorHandler mediatorHandler)
                : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            foreach (var type in modelBuilder.Model.GetEntityTypes().Where(e => typeof(Entity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(type.ClrType)
                    .HasKey("Id");

                modelBuilder
                    .Entity(type.ClrType)
                    .Property<DateTime>("CreatedAt")
                    .IsRequired();

                modelBuilder
                    .Entity(type.ClrType)
                    .Property<DateTime?>("ModifiedAt");
            }
        }
        private void SetDefaultValues()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("ModifiedAt").CurrentValue = DateTime.Now;
                }
            }
        }

        public async Task<bool> Commit()
        {
            SetDefaultValues();
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublishEvent(this);

            return sucesso;
        }
    }
}

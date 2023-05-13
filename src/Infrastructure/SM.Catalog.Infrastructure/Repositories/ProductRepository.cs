#nullable disable

using Microsoft.EntityFrameworkCore;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Core.Domain.Entities;
using SM.Catalog.Infrastructure.DbContexts;
using SM.Resource.Data;

namespace SM.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;
        private bool disposedValue;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Product.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            return (await _context.AddAsync(product)).Entity;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            await Task.CompletedTask;
            return (_context.Product.Update(product)).Entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

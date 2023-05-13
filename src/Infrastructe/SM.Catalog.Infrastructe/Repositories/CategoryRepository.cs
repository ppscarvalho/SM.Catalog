using Microsoft.EntityFrameworkCore;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Core.Domain.Entities;
using SM.Resource.Data;

namespace SM.Catalog.Infrastructe.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _context;
        private bool disposedValue;

        public CategoryRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _context.Category.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Category> SaveCategory(Category category)
        {
            return (await _context.AddAsync(category)).Entity;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            await Task.CompletedTask;
            _context.Entry(category).State = EntityState.Modified;
            return category;
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

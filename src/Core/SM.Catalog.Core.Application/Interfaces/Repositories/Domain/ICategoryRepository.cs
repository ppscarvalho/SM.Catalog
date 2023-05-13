using SM.Catalog.Core.Domain.Entities;
using SM.Resource.Data;

namespace SM.Catalog.Core.Application.Interfaces.Repositories.Domain
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(Guid id);
        Task<Category> SaveCategory(Category category);
        Task<Category> UpdateCategory(Category category);
    }
}

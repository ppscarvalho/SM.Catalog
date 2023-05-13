using SM.Catalog.Core.Domain.Entities;
using SM.Resource.Data;

namespace SM.Catalog.Core.Application.Interfaces.Repositories.Domain
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(Guid id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
    }
}

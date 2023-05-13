using SM.Catalog.Core.Domain.Entities;
using SM.Resource.Data;

namespace SM.Catalog.Core.Application.Interfaces.Repositories.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(Guid id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
    }
}

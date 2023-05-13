using MediatR;
using SM.Catalog.Core.Application.Models;

namespace SM.Catalog.Core.Application.Queries.Product
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductModel>>
    {

    }
}

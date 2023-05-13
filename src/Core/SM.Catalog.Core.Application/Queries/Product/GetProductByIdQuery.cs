using MediatR;
using SM.Catalog.Core.Application.Models;

namespace SM.Catalog.Core.Application.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductModel>
    {
        public Guid Id { get; private set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

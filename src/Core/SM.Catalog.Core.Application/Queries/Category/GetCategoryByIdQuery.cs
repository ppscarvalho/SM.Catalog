using MediatR;
using SM.Catalog.Core.Application.Models;

namespace SM.Catalog.Core.Application.Queries.Category
{
    public class GetCategoryByIdQuery : IRequest<CategoryModel>
    {
        public Guid Id { get; private set; }

        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

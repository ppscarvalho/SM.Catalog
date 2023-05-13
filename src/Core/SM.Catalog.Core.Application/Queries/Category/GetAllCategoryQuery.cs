using MediatR;
using SM.Catalog.Core.Application.Models;

namespace SM.Catalog.Core.Application.Queries.Category
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<CategoryModel>>
    {
    }
}

using AutoMapper;
using MediatR;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Core.Application.Models;

namespace SM.Catalog.Core.Application.Queries.Category
{
    public class CategoryQueryHandler :
        IRequestHandler<GetCategoryByIdQuery, CategoryModel>,
        IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryModel> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<CategoryModel>(await _categoryRepository.GetById(query.Id));
        }

        public async Task<IEnumerable<CategoryModel>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CategoryModel>>(await _categoryRepository.GetAll());
        }
    }
}

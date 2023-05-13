using AutoMapper;
using MediatR;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Core.Application.Models;

namespace SM.Catalog.Core.Application.Queries.Product
{
    public class ProductQueryHandler :
        IRequestHandler<GetProductByIdQuery, ProductModel>,
        IRequestHandler<GetAllProductQuery, IEnumerable<ProductModel>>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;
        public ProductQueryHandler(IProductRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<ProductModel> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<ProductModel>(await _ProductRepository.GetProductById(query.Id));
        }

        public async Task<IEnumerable<ProductModel>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ProductModel>>(await _ProductRepository.GetAllProduct());
        }
    }
}

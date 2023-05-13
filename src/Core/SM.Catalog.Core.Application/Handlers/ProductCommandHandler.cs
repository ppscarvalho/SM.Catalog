using AutoMapper;
using MediatR;
using SM.Catalog.Core.Application.Commands.Product;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Domain.Entities;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Catalog.Core.Application.Handlers
{
    public class ProductCommandHandler :
    IRequestHandler<AddProductCommand, DefaultResult>,
    IRequestHandler<UpdateProductCommand, DefaultResult>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public ProductCommandHandler(
            IProductRepository ProductRepository,
            IMediatorHandler mediatorHandler,
            IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<DefaultResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var product = _mapper.Map<Product>(request);
            product.Enbled();
            var entity = _mapper.Map<ProductModel>(await _ProductRepository.AddProduct(product));

            var result = await _ProductRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result };
        }

        public async Task<DefaultResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var product = _mapper.Map<Product>(request);
            product.Enbled();
            var entity = _mapper.Map<ProductModel>(await _ProductRepository.UpdateProduct(product));

            var result = await _ProductRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result };
        }

        private bool ValidateCommand(CommandHandler message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }
    }
}

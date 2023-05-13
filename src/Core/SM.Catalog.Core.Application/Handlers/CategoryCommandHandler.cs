using AutoMapper;
using MediatR;
using SM.Catalog.Core.Application.Commands.Category;
using SM.Catalog.Core.Application.Interfaces.Repositories.Domain;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Domain.Entities;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Catalog.Core.Application.Handlers
{
    public class CategoryCommandHandler :
    IRequestHandler<AddCategoryCommand, DefaultResult>,
        IRequestHandler<UpdateCategoryCommand, DefaultResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public CategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IMediatorHandler mediatorHandler,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<DefaultResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var categoria = _mapper.Map<Category>(request);
            var entity = _mapper.Map<CategoryModel>(await _categoryRepository.SaveCategory(categoria));

            var result = await _categoryRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result };
        }

        public async Task<DefaultResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var categoria = _mapper.Map<Category>(request);
            var entity = _mapper.Map<CategoryModel>(await _categoryRepository.UpdateCategory(categoria));

            var result = await _categoryRepository.UnitOfWork.Commit();

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

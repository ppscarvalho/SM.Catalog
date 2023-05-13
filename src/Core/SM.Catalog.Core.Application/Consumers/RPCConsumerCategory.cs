#nullable disable

using AutoMapper;
using MediatR;
using SM.Catalog.Core.Application.Commands.Category;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Category;
using SM.MQ.Models;
using SM.MQ.Models.Categoria;
using SM.MQ.Operators;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Util.Extensions;

namespace SM.Catalog.Core.Application.Consumers
{
    public class RPCConsumerCategory : Consumer<RequestIn>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;
        private readonly DomainNotificationHandler _notifications;

        public RPCConsumerCategory(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            INotificationHandler<DomainNotification> notifications,
            IMediator mediatorQuery)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorQuery = mediatorQuery;
        }

        public override async Task ConsumeContex(ConsumerContext<RequestIn> context)
        {
            switch (context.Message.Queue)
            {
                case "GetCategoryById":
                    await GetCategoryById(context);
                    break;

                case "GetAllCategory":
                    await GetAllCategory(context);
                    break;

                case "AddCategory":
                    await AddCategory(context);
                    break;

                case "UpdateCategory":
                    await UpdateCategoria(context);
                    break;

                default:
                    await GetAllCategory(context);
                    break;
            }
        }


        private async Task GetCategoryById(ConsumerContext<RequestIn> context)
        {
            var id = Guid.Parse(context.Message.Result);
            var query = new GetCategoryByIdQuery(id);
            var result = _mapper.Map<ResponseCategoriaOut>(await _mediatorQuery.Send(query));
            await context.RespondAsync(result);
        }
        private async Task GetAllCategory(ConsumerContext<RequestIn> context)
        {
            var result = _mapper.Map<IEnumerable<ResponseCategoriaOut>>(await _mediatorQuery.Send(new GetAllCategoryQuery()));
            await context.RespondAsync(result.ToArray());
        }

        private async Task AddCategory(ConsumerContext<RequestIn> context)
        {
            var categoryModel = context.Message.Result.DeserializeObject<CategoryModel>();

            var command = _mapper.Map<AddCategoryCommand>(categoryModel);
            var result = await _mediatorHandler.SendCommand(command);

            if (result.Success)
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
            else if (!_notifications.ExistNotification())
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
        }

        private async Task UpdateCategoria(ConsumerContext<RequestIn> context)
        {
            var categoriaModel = context.Message.Result.DeserializeObject<CategoryModel>();

            var command = _mapper.Map<UpdateCategoryCommand>(categoriaModel);
            var result = await _mediatorHandler.SendCommand(command);

            if (result.Success)
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
            else if (!_notifications.ExistNotification())
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
        }
    }
}

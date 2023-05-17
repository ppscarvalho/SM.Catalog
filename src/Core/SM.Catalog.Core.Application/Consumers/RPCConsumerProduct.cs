#nullable disable

using AutoMapper;
using MediatR;
using SM.Catalog.Core.Application.Commands.Product;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Product;
using SM.MQ.Models;
using SM.MQ.Models.Product;
using SM.MQ.Operators;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Util.Extensions;

namespace SM.Catalog.Core.Application.Consumers
{
    public class RPCConsumerProduct : Consumer<RequestIn>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;
        private readonly DomainNotificationHandler _notifications;

        public RPCConsumerProduct(
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
                case "GetProductById":
                    await GetProductById(context);
                    break;

                case "GetAllProduct":
                    await GetAllProduct(context);
                    break;

                case "AddProduct":
                    await AddProduct(context);
                    break;

                case "UpdateProduct":
                    await UpdateProduct(context);
                    break;

                default:
                    await GetAllProduct(context);
                    break;
            }
        }


        private async Task GetProductById(ConsumerContext<RequestIn> context)
        {
            var id = Guid.Parse(context.Message.Result);
            var query = new GetProductByIdQuery(id);
            var result = _mapper.Map<ResponseProductOut>(await _mediatorQuery.Send(query));
            await context.RespondAsync(result);
        }
        private async Task GetAllProduct(ConsumerContext<RequestIn> context)
        {
            var result = _mapper.Map<IEnumerable<ResponseProductOut>>(await _mediatorQuery.Send(new GetAllProductQuery()));
            await context.RespondAsync(result.ToArray());
        }

        private async Task AddProduct(ConsumerContext<RequestIn> context)
        {
            var productModel = context.Message.Result.DeserializeObject<ProductModel>();
            var command = _mapper.Map<AddProductCommand>(productModel);
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

        private async Task UpdateProduct(ConsumerContext<RequestIn> context)
        {
            var productModel = context.Message.Result.DeserializeObject<ProductModel>();
            var command = _mapper.Map<UpdateProductCommand>(productModel);
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

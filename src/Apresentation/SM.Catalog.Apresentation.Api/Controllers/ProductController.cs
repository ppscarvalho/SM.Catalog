using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Catalog.Apresentation.Api.Controllers.BaseController;
using SM.Catalog.Core.Application.Commands.Product;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Product;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Catalog.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerConfiguration
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;

        public ProductController(
            ILogger<ProductController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IMapper mapper,
            IMediator mediatorQuery) : base(notifications, mediatorHandler)
        {
            _logger = logger;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _mediatorQuery = mediatorQuery;
        }

        [HttpPost]
        [Route("GetProductById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById([FromBody] GetProductByIdQuery query)
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetAllProduct")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProduct()
        {
            _logger.LogInformation("Obter todas os produtos");
            var result = await _mediatorQuery.Send(new GetAllProductQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> AddProduct([FromBody] ProductModel ProductModel)
        {
            var cmd = _mapper.Map<AddProductCommand>(ProductModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }

        [HttpPost]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> UpdateProduct([FromBody] ProductModel ProductModel)
        {
            var cmd = _mapper.Map<UpdateProductCommand>(ProductModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Catalog.Apresentation.Api.Controllers.BaseController;
using SM.Catalog.Core.Application.Commands.Category;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Category;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Catalog.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerConfiguration
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;

        public CategoryController(
            ILogger<CategoryController> logger,
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
        [Route("GetCategoryById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById([FromBody] GetCategoryByIdQuery query)
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory()
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(new GetAllCategoryQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("AddCategory")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> SaveCategory([FromBody] CategoryModel categoryModel)
        {
            var cmd = _mapper.Map<AddCategoryCommand>(categoryModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }

        [HttpPost]
        [Route("UpdateCategory")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> UpdateCategory([FromBody] CategoryModel categoryModel)
        {
            var cmd = _mapper.Map<UpdateCategoryCommand>(categoryModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }
    }
}

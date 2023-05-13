using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Category;
using SM.MQ.Models;
using SM.MQ.Models.Categoria;
using SM.MQ.Operators;

namespace SM.Catalog.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPCCategoryController : ControllerBase
    {
        private readonly ILogger<RPCCategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublish _publish;

        public RPCCategoryController(
            ILogger<RPCCategoryController> logger,
            IMapper mapper,
            IPublish publish)
        {
            _logger = logger;
            _mapper = mapper;
            _publish = publish;
        }

        [HttpPost]
        [Route("GetCategoryById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseCategoriaOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById([FromBody] GetCategoryByIdQuery query)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = query.Id.ToString(),
                Queue = "GetCategoryById"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseCategoriaOut>(mapIn);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetAllCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseCategoriaOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory()
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Queue = "GetAllCategory"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseCategoriaOut[]>(mapIn);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddCategory")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> AddCategory([FromBody] CategoryModel categoryModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(categoryModel),
                Queue = "AddCategory"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }

        [HttpPost]
        [Route("UpdateCategory")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> UpdateCategory([FromBody] CategoryModel categoryModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(categoryModel),
                Queue = "UpdateCategory"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }
    }
}

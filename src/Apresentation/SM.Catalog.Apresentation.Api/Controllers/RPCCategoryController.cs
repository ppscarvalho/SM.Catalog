using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Category;
using SM.MQ.Models;
using SM.MQ.Models.Category;
using SM.MQ.Operators;

namespace SM.Catalog.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPCCategoryController : ControllerBase
    {
        private readonly IPublish _publish;

        public RPCCategoryController(IPublish publish)
        {
            _publish = publish;
        }

        [HttpPost]
        [Route("GetCategoryById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseCategoryOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById([FromBody] GetCategoryByIdQuery query)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = query.Id.ToString(),
                Queue = "GetCategoryById"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseCategoryOut>(mapIn);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseCategoryOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory()
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Queue = "GetAllCategory"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseCategoryOut[]>(mapIn);
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

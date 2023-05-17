using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Application.Queries.Product;
using SM.MQ.Models;
using SM.MQ.Models.Product;
using SM.MQ.Operators;

namespace SM.Catalog.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPCProductController : ControllerBase
    {
        private readonly IPublish _publish;

        public RPCProductController(IPublish publish)
        {
            _publish = publish;
        }

        [HttpPost]
        [Route("GetProductById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseProductOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById([FromBody] GetProductByIdQuery query)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = query.Id.ToString(),
                Queue = "GetProductById"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseProductOut>(mapIn);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProduct")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseProductOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProduct()
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Queue = "GetAllProduct"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseProductOut[]>(mapIn);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> AddProduct([FromBody] ProductModel ProductModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(ProductModel),
                Queue = "AddProduct"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }

        [HttpPost]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> UpdateProduct([FromBody] ProductModel ProductModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(ProductModel),
                Queue = "UpdateProduct"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }
    }
}

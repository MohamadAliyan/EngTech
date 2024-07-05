using EngTech.Application.Contract.Commands.Products;
using EngTech.Application.Contract.Queries.Products;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        int result = await Mediator.Send(request);
        return Dynamic(result);
    }

    [HttpGet("GetProduct")]
    public async Task<IActionResult> GetProduct([FromQuery] GetProductByIdQuery request)
    {
        GetProductByIdDto result = await Mediator.Send(request);
        return Dynamic(result);
    }

    [HttpPost("UpdateProduct")]
    public async Task<IActionResult> GetProduct([FromBody] UpdateProductCommand request)
    {
        int result = await Mediator.Send(request);
        return Dynamic(result);
    }
}
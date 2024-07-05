using EngTech.Application.Contract.Commands.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpPost("Buy")]
    public async Task<IActionResult> Buy([FromBody] CreateOrderCommand request)
    {
        int result = await Mediator.Send(request);
        return Dynamic(result);
    }
}
using EngTech.Application.Contract.Queries.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UserController : ApiControllerBase
{
    [HttpGet("GetAllUser")]
    public async Task<IActionResult> GetUser([FromQuery] GetAllUserQuery request)
    {
        List<UserDto> result = await Mediator.Send(request);
        return Dynamic(result);
    }
}
using Api.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    #region .:: RESPONSE HELPERS ::.

    [NonAction]
    protected StandardBaseResponse<TData> Dynamic<TData>(TData response)
    {
        return new StandardBaseResponse<TData>(response);
    }

    [NonAction]
    protected StandardBaseResponse<TData> Dynamic<TData>()
    {
        return new StandardBaseResponse<TData>();
    }

    #endregion
}

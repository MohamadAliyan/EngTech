using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Helper;

[DefaultStatusCode(DefaultStatusCode)]
public class StandardBaseResponse<TData> : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status200OK;

    public StandardBaseResponse(TData response) : base(new StandardBaseResponseContract<TData>
    {
        Data = response, Result = new StandardBaseResultContract()
    })
    {
    }

    public StandardBaseResponse() : base(new StandardBaseResponseContract<TData>
    {
        Data = default, Result = new StandardBaseResultContract()
    })
    {
        StatusCode = DefaultStatusCode;
    }
}

public class StandardBaseResponseContract
{
    public StandardBaseResultContract Result { get; set; }
}

public class StandardBaseResponseContract<TData> : StandardBaseResponseContract
{
    public TData Data { get; set; }
}

public class StandardBaseResultContract
{
    public string Key { get; set; } = "Success";
    public int SituationCode { get; set; }
    public string Message { get; set; }
    public string Description { get; set; }
    public IList<string> Errors { get; set; }
}





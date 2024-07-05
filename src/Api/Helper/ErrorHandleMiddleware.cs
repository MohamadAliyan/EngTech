using System.Text.Json;

namespace Api.Helper;

public class ErrorHandleMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ErrorHandleMiddleware(RequestDelegate next, ILogger<ErrorHandleMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IHttpContextAccessor contextAccessor)
    {
        try
        {
            await _next(context);
        }

        catch (Exception exception)
        {
            Console.WriteLine("Exception =>" + exception);
            await Handle(context, exception, contextAccessor);
        }
    }

    public async Task Handle(HttpContext httpContext, Exception ex,
        IHttpContextAccessor contextAccessor)
    {
        httpContext.Response.ContentType = "application/problem+json";

        StandardBaseResultContract contract = new()
        {
            Key = "Unprocessable", Errors = new List<string>()
        };


        if (ex is Exception exceptionEx)
        {
            contract.Message = ex.Message;

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }

        StandardBaseResponseContract problem = new() { Result = contract };

        string errors = problem.Result.Errors.Any()
            ? problem.Result.Errors.Aggregate((a, b) => a + "," + b)
            : string.Empty;


        Stream stream = httpContext.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problem,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}

public static class ErrorHandlerExtensions
{
    public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandleMiddleware>();
    }
}
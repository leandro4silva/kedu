using Kedu.Api.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Filters.Handlers;

public class ArgumentExceptionHandler : IExceptionHandler
{
    public ObjectResult Handle(Exception exception)
    {
        var details = new ProblemDetails
        {
            Title = "Bad Request Error",
            Status = StatusCodes.Status400BadRequest,
            Type = "BadRequest",
            Detail = exception.Message
        };

        return new ObjectResult(details) { StatusCode = details.Status };
    }
}
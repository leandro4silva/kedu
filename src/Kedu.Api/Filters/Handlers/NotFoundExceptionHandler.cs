using Kedu.Api.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Filters.Handlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public ObjectResult Handle(Exception exception)
    {
        var details = new ProblemDetails
        {
            Title = "Not found",
            Status = StatusCodes.Status404NotFound,
            Type = "NotFound",
            Detail = exception.Message
        };

        return new ObjectResult(details) { StatusCode = details.Status };
    }
}
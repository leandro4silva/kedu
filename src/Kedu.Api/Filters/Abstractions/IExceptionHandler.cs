using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Filters.Abstractions;

public interface IExceptionHandler
{
    ObjectResult Handle(Exception exception);
}
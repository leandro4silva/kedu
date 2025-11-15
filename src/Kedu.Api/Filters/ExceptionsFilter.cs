
using FluentValidation;
using Kedu.Api.Filters.Abstractions;
using Kedu.Api.Filters.Handlers;
using Kedu.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kedu.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly Dictionary<Type, IExceptionHandler> _exceptionHandlers;

    public ExceptionFilter()
    {
        _exceptionHandlers = new Dictionary<Type, IExceptionHandler>
        {
            { typeof(ValidationException), new ValidationExceptionHandler() },
            { typeof(ArgumentException), new ArgumentExceptionHandler() },
            { typeof(NotFoundException), new NotFoundExceptionHandler() },
            { typeof(InternalServerErrorException), new InternalServerErrorExceptionHandler() }
        };
    }

    public void OnException(ExceptionContext context)
    {
        var exceptionType = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            var handler = _exceptionHandlers[exceptionType];
            context.Result = handler.Handle(context.Exception);
        }
        else
        {
            context.Result = new UnexpectedExceptionHandler().Handle(context.Exception);
        }

        context.ExceptionHandled = true;
    }
}
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Middleware
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ProblemDetails details;

            if (exception is ValidationException validation)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                details = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = validation.Message,
                    Instance = httpContext.Request.Path
                };
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                details = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = exception.Message ?? "Ocurrió un error inesperado.",
                    Instance = httpContext.Request.Path
                };
            }

            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

            return true;
        }
    }
}
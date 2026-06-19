using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Middleware
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is ValidationException validation)
            {
                var details = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = validation.Message,
                    Instance = httpContext.Request.Path
                };

                await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
                return true;
            }
            
            return false;
        }
    }
}

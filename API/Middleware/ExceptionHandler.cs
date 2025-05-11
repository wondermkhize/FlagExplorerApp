using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Middleware;

public static class ExceptionHandler
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                context.Response.StatusCode = exception switch
                {
                    CountryNotFoundException => (int)HttpStatusCode.NotFound,
                    ExternalApiException => (int)HttpStatusCode.ServiceUnavailable,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var errorResponse = new
                {
                    error = exception?.Message ?? "An unexpected error occurred."
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            });
        });
    }
}

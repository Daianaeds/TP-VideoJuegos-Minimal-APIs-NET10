using Microsoft.AspNetCore.Diagnostics;

namespace BibliotecaVideojuegosApi.Errores;

public class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        logger.LogError(
            exception,
            "Excepción no manejada al procesar {Metodo} {Ruta}",
            httpContext.Request.Method, httpContext.Request.Path);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "Ocurrió un error inesperado",
                Detail = "El servidor no pudo procesar la petición. Intenta de nuevo más tarde.",
                Status = StatusCodes.Status500InternalServerError
            }
        });
    }
}




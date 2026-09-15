using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Services;

namespace BibliotecaVideojuegosApi.Endpoints;

public static class CopiaVideoJuegoEndpoints
{
    public static void MapCopiaVideoJuegoEndpoints(this IEndpointRouteBuilder app)
    {
        var groupCopias = app.MapGroup("/copiasvideojuego");

        groupCopias.MapGet("/", ObtenerCopiasVideoJuego);
        groupCopias.MapGet("/{id:int}", ObtenerCopiaVideoJuegoPorId);
        groupCopias.MapPost("/", CrearCopiaVideoJuego);
        groupCopias.MapPut("/{id:int}", ActualizarCopiaVideoJuego);
        groupCopias.MapDelete("/{id:int}", EliminarCopiaVideoJuego);

        static async Task<IResult> ObtenerCopiasVideoJuego(CopiaVideoJuegoService copiaVideoJuegoService)
        {
            var copias = await copiaVideoJuegoService.GetAllCopiaVideoJuego();
            return TypedResults.Ok(copias);
        }

        static async Task<IResult> ObtenerCopiaVideoJuegoPorId(int id, CopiaVideoJuegoService copiaVideoJuegoService)
        {
            var copia = await copiaVideoJuegoService.GetCopiaVideoJuegoById(id);
            return copia is not null ? TypedResults.Ok(copia) : TypedResults.NotFound();
        }

        static async Task<IResult> CrearCopiaVideoJuego(CopiaVideoJuegoRequestDto request, CopiaVideoJuegoService copiaVideoJuegoService)
        {
            if (request is null || request.VideoJuegoId <= 0)
            {
                return TypedResults.BadRequest();
            }

            var copiaCreada = await copiaVideoJuegoService.AgregarCopiasVideoJuego(request);
            return TypedResults.Created($"/copiasvideojuego/{copiaCreada.Id}", copiaCreada);
        }

        static async Task<IResult> ActualizarCopiaVideoJuego(int id, CopiaVideoJuegoRequestDto request, CopiaVideoJuegoService copiaVideoJuegoService)
        {
            if (request is null || request.VideoJuegoId <= 0)
            {
                return TypedResults.BadRequest();
            }

            var actualizado = await copiaVideoJuegoService.ActualizarCopiaVideoJuego(id, request);
            return actualizado ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        static async Task<IResult> EliminarCopiaVideoJuego(int id, CopiaVideoJuegoService copiaVideoJuegoService)
        {
            var eliminado = await copiaVideoJuegoService.EliminarCopiaVideoJuego(id);
            return eliminado ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}

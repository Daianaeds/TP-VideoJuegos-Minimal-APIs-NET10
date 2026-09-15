using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Services;

namespace BibliotecaVideojuegosApi.Endpoints;

public static class VideoJuegosEndpoints
{
    public static void MapVideoJuegosEndpoints(this IEndpointRouteBuilder app)
    {
        var groupJuegos = app.MapGroup("/videojuegos");

        groupJuegos.MapGet("/", ObtenerVideosJuegos);
        groupJuegos.MapGet("/{id:int}", ObtenerVideoJuegoPorId);
        groupJuegos.MapPost("/", CrearVideoJuego);
        groupJuegos.MapPut("/{id:int}", ActualizarVideoJuego);
        groupJuegos.MapDelete("/{id:int}", EliminarVideoJuego);

        static async Task<IResult> ObtenerVideosJuegos(VideoJuegoService videoJuegosService)
        {
            var videojuegos = await videoJuegosService.GetVideosJuegos();
            return TypedResults.Ok(videojuegos);
        }

        static async Task<IResult> ObtenerVideoJuegoPorId(int id, VideoJuegoService videoJuegosService)
        {
            var videojuego = await videoJuegosService.GetVideoJuegoById(id);
            return videojuego is not null ? TypedResults.Ok(videojuego) : TypedResults.NotFound();
        }

        static async Task<IResult> CrearVideoJuego(VideoJuegoRequestDto request, VideoJuegoService videoJuegosService)
        {
            if (request is null)
            {
                return TypedResults.BadRequest();
            }

            var videojuegoCreado = await videoJuegosService.CreateVideoJuego(request);
            return TypedResults.Created($"/videojuegos/{videojuegoCreado.Id}", videojuegoCreado);
        }

        static async Task<IResult> ActualizarVideoJuego(int id, VideoJuegoRequestDto request, VideoJuegoService videoJuegosService)
        {
            if (request is null)
            {
                return TypedResults.BadRequest();
            }

            var actualizado = await videoJuegosService.ActualizarVideoJuego(id, request);
            return actualizado ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        static async Task<IResult> EliminarVideoJuego(int id, VideoJuegoService videoJuegosService)
        {
            var eliminado = await videoJuegosService.EliminarVideoJuego(id);
            return eliminado ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
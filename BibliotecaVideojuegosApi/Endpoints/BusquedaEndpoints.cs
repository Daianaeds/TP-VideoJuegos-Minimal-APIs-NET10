using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Services;
using BibliotecaVideojuegosApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVideojuegosApi.Endpoints;

public static class BusquedaEndpoints
{
    public static void MapBusquedaEndpoints(this IEndpointRouteBuilder app)
    {
        var groupJuegos = app.MapGroup("/videojuegos");

        groupJuegos.MapQuery("/buscar", Buscar)
           .ExcludeFromDescription();
    }

    static async Task<IResult> Buscar(
        [FromBody] BusquedaVideoJuegoDto criterios,
        VideoJuegoService service)
    {
        if (criterios is null)
        {
            return TypedResults.BadRequest();
        }

        var resultado = await service.Buscar(criterios);

        return TypedResults.Ok(resultado);
    }
}

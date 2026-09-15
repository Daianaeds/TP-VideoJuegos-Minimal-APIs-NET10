using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Services;

namespace BibliotecaVideojuegosApi.Endpoints;

public static class PrestamoVideoJuegoEndpoints
{
    public static void MapPrestamoVideoJuegoEndpoints(this IEndpointRouteBuilder app)
    {
        var groupPrestamos = app.MapGroup("/prestamovideojuego");

        groupPrestamos.MapGet("/", ObtenerPrestamos);
        groupPrestamos.MapGet("/{id:int}", ObtenerPrestamoPorId);
        groupPrestamos.MapPost("/", CrearPrestamo);
        groupPrestamos.MapPut("/{id:int}/devolver", DevolverPrestamo);
        groupPrestamos.MapDelete("/{id:int}", EliminarPrestamo);

        static async Task<IResult> ObtenerPrestamos(PrestamoVideoJuegoService prestamoVideoJuegoService)
        {
            var prestamos = await prestamoVideoJuegoService.GetAllPrestamos();
            return TypedResults.Ok(prestamos);
        }

        static async Task<IResult> ObtenerPrestamoPorId(int id, PrestamoVideoJuegoService prestamoVideoJuegoService)
        {
            var prestamo = await prestamoVideoJuegoService.GetPrestamoId(id);
            return prestamo is not null ? TypedResults.Ok(prestamo) : TypedResults.NotFound();
        }

        static async Task<IResult> CrearPrestamo(PrestamoVideoJuegoRequestDto request, PrestamoVideoJuegoService prestamoVideoJuegoService)
        {
            if (request is null || request.CopiaVideoJuegoId <= 0 || string.IsNullOrWhiteSpace(request.NombreUsuario))
            {
                return TypedResults.BadRequest();
            }

            var prestamoCreado = await prestamoVideoJuegoService.CrearPrestamo(request);
            return prestamoCreado is null
                ? TypedResults.BadRequest()
                : TypedResults.Created($"/prestamovideojuego/{prestamoCreado.Id}", prestamoCreado);
        }

        static async Task<IResult> DevolverPrestamo(int id, PrestamoVideoJuegoService prestamoVideoJuegoService)
        {
            var actualizado = await prestamoVideoJuegoService.DevolverVideoJuego(id);
            return actualizado ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        static async Task<IResult> EliminarPrestamo(int id, PrestamoVideoJuegoService prestamoVideoJuegoService)
        {
            var eliminado = await prestamoVideoJuegoService.EliminarPrestamo(id);
            return eliminado ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}

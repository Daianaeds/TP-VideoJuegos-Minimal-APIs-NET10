using BibliotecaVideojuegosApi.Data;
using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaVideojuegosApi.Services;

public class PrestamoVideoJuegoService(AppDbContext db)
{
    public async Task<bool> PrestarVideoJuego(int copiaVideoJuegoId, string nombreUsuario)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario))
        {
            return false;
        }

        var copiaVideoJuego = await db.CopiaVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(cvj => cvj.Id == copiaVideoJuegoId);

        if (copiaVideoJuego is not null && !copiaVideoJuego.EstadoPrestado)
        {
            var prestamo = new PrestamoJuego
            {
                CopiaVideoJuegoId = copiaVideoJuegoId,
                FechaPrestamo = DateTime.UtcNow,
                NombreUsuario = nombreUsuario
            };
            db.PrestamoVideoJuego.Add(prestamo);

            copiaVideoJuego.EstadoPrestado = true;
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> DevolverVideoJuego(int prestamoId)
    {
        var prestamo = await db.PrestamoVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == prestamoId);

        if (prestamo == null || prestamo.FechaDevolucion != null)
        {
            return false;
        }

        var copiaVideoJuego = await db.CopiaVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(cvj => cvj.Id == prestamo.CopiaVideoJuegoId);

        if (copiaVideoJuego is null)
        {
            return false;
        }

        prestamo.FechaDevolucion = DateTime.UtcNow;
        copiaVideoJuego.EstadoPrestado = false;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<PrestamoJuego?> GetPrestamoId(int prestamoId)
    {
        return await db.PrestamoVideoJuego
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == prestamoId);
    }

    public async Task<List<PrestamoJuego>> GetAllPrestamos()
    {
        return await db.PrestamoVideoJuego
            .AsNoTracking()
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }
}
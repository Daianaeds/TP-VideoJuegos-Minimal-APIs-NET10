using BibliotecaVideojuegosApi.Data;
using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaVideojuegosApi.Services;

public class PrestamoVideoJuegoService(AppDbContext db)
{
    public async Task<PrestamoJuego?> CrearPrestamo(PrestamoVideoJuegoRequestDto request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.NombreUsuario))
        {
            return null;
        }

        var copiaVideoJuego = await db.CopiaVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(cvj => cvj.Id == request.CopiaVideoJuegoId);

        if (copiaVideoJuego is null || copiaVideoJuego.EstadoPrestado)
        {
            return null;
        }

        var prestamo = new PrestamoJuego
        {
            CopiaVideoJuegoId = request.CopiaVideoJuegoId,
            FechaPrestamo = request.FechaPrestamo == default ? DateTime.UtcNow : request.FechaPrestamo,
            NombreUsuario = request.NombreUsuario.Trim()
        };

        db.PrestamoVideoJuego.Add(prestamo);
        copiaVideoJuego.EstadoPrestado = true;
        await db.SaveChangesAsync();

        return prestamo;
    }

    public async Task<bool> PrestarVideoJuego(int copiaVideoJuegoId, string nombreUsuario)
    {
        var prestamo = await CrearPrestamo(new PrestamoVideoJuegoRequestDto
        {
            CopiaVideoJuegoId = copiaVideoJuegoId,
            FechaPrestamo = DateTime.UtcNow,
            NombreUsuario = nombreUsuario
        });

        return prestamo is not null;
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

    public async Task<bool> EliminarPrestamo(int prestamoId)
    {
        var prestamo = await db.PrestamoVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == prestamoId);

        if (prestamo is null)
        {
            return false;
        }

        db.PrestamoVideoJuego.Remove(prestamo);
        await db.SaveChangesAsync();
        return true;
    }
}
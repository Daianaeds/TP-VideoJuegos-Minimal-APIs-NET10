using BibliotecaVideojuegosApi.Data;
using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVideojuegosApi.Services;

public class CopiaVideoJuegoService(AppDbContext db, ILogger<CopiaVideoJuegoService> logger)
{
    public async Task<CopiasVideosJuegosResponseDto> AgregarCopiasVideoJuego(CopiaVideoJuegoRequestDto copiaVideoJuegoRequestDto)
    {
        var nuevoVideoJuego = new CopiaVideoJuego 
        {
            VideoJuegoId = copiaVideoJuegoRequestDto.VideoJuegoId,
            EstadoPrestado = copiaVideoJuegoRequestDto.EstadoPrestado
        };

        db.CopiaVideoJuego.Add(nuevoVideoJuego);
        await db.SaveChangesAsync();

        logger.LogInformation(
            $"Copia de videojuego creada: {nuevoVideoJuego.Id} para VideoJuegoId {nuevoVideoJuego.VideoJuegoId}");

        var videoJuego = await db.VideoJuego
            .AsNoTracking()
            .Where(vj => vj.Id == nuevoVideoJuego.VideoJuegoId)
            .Select(vj => new VideoJuegoResponseDto
            {
                Id = vj.Id,
                Nombre = vj.Titulo,
                Genero = vj.Genero,
                Plataforma = vj.Plataforma
            })
            .FirstOrDefaultAsync();

        return new CopiasVideosJuegosResponseDto
        {
            Id = nuevoVideoJuego.Id,
            VideoJuegoId = nuevoVideoJuego.VideoJuegoId,
            EstadoPrestado = nuevoVideoJuego.EstadoPrestado,
            VideoJuego = videoJuego
        };
    }

    public async Task<CopiasVideosJuegosResponseDto?> GetCopiaVideoJuegoById(int id)
    {
        return await db.CopiaVideoJuego
            .AsNoTracking()
            .Where(cvj => cvj.Id == id)
            .Select(cvj => new CopiasVideosJuegosResponseDto
            {
                Id = cvj.Id,
                VideoJuegoId = cvj.VideoJuegoId,
                EstadoPrestado = cvj.EstadoPrestado,
                VideoJuego = db.VideoJuego
                    .Where(vj => vj.Id == cvj.VideoJuegoId)
                    .Select(vj => new VideoJuegoResponseDto
                    {
                        Id = vj.Id,
                        Nombre = vj.Titulo,
                        Genero = vj.Genero,
                        Plataforma = vj.Plataforma
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<CopiasVideosJuegosResponseDto>> GetAllCopiaVideoJuego()
    {
        return await db.CopiaVideoJuego
            .AsNoTracking()
            .Select(cvj => new CopiasVideosJuegosResponseDto
            {
                Id = cvj.Id,
                VideoJuegoId = cvj.VideoJuegoId,
                EstadoPrestado = cvj.EstadoPrestado,
                VideoJuego = db.VideoJuego
                    .Where(vj => vj.Id == cvj.VideoJuegoId)
                    .Select(vj => new VideoJuegoResponseDto
                    {
                        Id = vj.Id,
                        Nombre = vj.Titulo,
                        Genero = vj.Genero,
                        Plataforma = vj.Plataforma
                    })
                    .FirstOrDefault()
            })
            .ToListAsync();
    }
    
    public async Task<bool> ActualizarCopiaVideoJuego(int id, CopiaVideoJuegoRequestDto copiaVideoJuegoRequest)
    {
        var copiaVideoJuego = await db.CopiaVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(cvj => cvj.Id == id);

        if (copiaVideoJuego is not null)
        {
            copiaVideoJuego.VideoJuegoId = copiaVideoJuegoRequest.VideoJuegoId;
            copiaVideoJuego.EstadoPrestado = copiaVideoJuegoRequest.EstadoPrestado;
            await db.SaveChangesAsync();
            logger.LogInformation($"Copia de videojuego actualizada: {id}");
            return true;
        }

        return false;
    }

    public async Task<bool> ActualizarEstadoPrestamoCopiaVideoJuego(int id, bool estadoPrestado)
    {
        var copiaVideoJuego = await db.CopiaVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(cvj => cvj.Id == id);
        if (copiaVideoJuego is not null)
        {
            copiaVideoJuego.EstadoPrestado = estadoPrestado;
            await db.SaveChangesAsync();
            logger.LogInformation($"Estado de préstamo actualizado para copia {id}: {estadoPrestado}");
            return true;
        }
        return false;
    }   

    public async Task<bool> EliminarCopiaVideoJuego(int id)
    {
        var copiaVideoJuego = await db.CopiaVideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(cvj => cvj.Id == id);
        if (copiaVideoJuego is not null)
        { 
            db.CopiaVideoJuego.Remove(copiaVideoJuego);
            await db.SaveChangesAsync();
            logger.LogInformation($"Copia de videojuego eliminada: {id}");
            return true;
        }

        return false;
    }
}
using BibliotecaVideojuegosApi.Data;
using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVideojuegosApi.Services;

public class VideoJuegoService(AppDbContext db)
{
    public async Task<VideoJuegoResponseDto> CreateVideoJuego(VideoJuegoRequestDto videoJuegoRequest)
    {
        var nuevoVideoJuego = new VideoJuego 
        { 
            Titulo = videoJuegoRequest.Titulo, 
            Genero = videoJuegoRequest.Genero, 
            Plataforma = videoJuegoRequest.Plataforma 
        };

        db.VideoJuego.Add(nuevoVideoJuego);
        await db.SaveChangesAsync();

        return new VideoJuegoResponseDto
        {
            Id = nuevoVideoJuego.Id,
            Nombre = nuevoVideoJuego.Titulo,
            Genero = nuevoVideoJuego.Genero,
            Plataforma = nuevoVideoJuego.Plataforma
        };
    }

    public async Task<VideoJuegoResponseDto?> GetVideoJuego(int id)
    {
        return await db.VideoJuego
            .AsNoTracking()
            .Where(vj => vj.Id == id)
            .Select(vj => new VideoJuegoResponseDto
            {
                Id = vj.Id,
                Nombre = vj.Titulo,
                Genero = vj.Genero,
                Plataforma = vj.Plataforma
            })
            .FirstOrDefaultAsync();
    }
    public async Task<List<VideoJuegoResponseDto>> GetVideosJuegos()
    {
        return await db.VideoJuego
            .AsNoTracking()
            .Select(vj => new VideoJuegoResponseDto
            {
                Id = vj.Id,
                Nombre = vj.Titulo,
                Genero = vj.Genero,
                Plataforma = vj.Plataforma
            })
            .ToListAsync();
    }
    public async Task<bool> ActualizarVideoJuego(int id, VideoJuegoRequestDto videoJuegoRequest)
    {
        var videoJuego = await db.VideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(vj => vj.Id == id);

        if (videoJuego is not null)
        {
            videoJuego.Titulo = videoJuegoRequest.Titulo;
            videoJuego.Genero = videoJuegoRequest.Genero;
            videoJuego.Plataforma = videoJuegoRequest.Plataforma;
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> EliminarVideoJuego(int id)
    {
        var videoJuego = await db.VideoJuego
            .AsTracking()
            .FirstOrDefaultAsync(vj => vj.Id == id);
        if (videoJuego is not null)
        { 
            db.VideoJuego.Remove(videoJuego);
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
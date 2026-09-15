using BibliotecaVideojuegosApi.Data;
using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVideojuegosApi.Services;

public class VideoJuegoService(AppDbContext db)
{
    public async Task<VideoJuegoResponseDto?> CreateVideoJuego(VideoJuegoRequestDto videoJuegoRequest)
    {
        var existeDuplicado = await db.VideoJuego.AnyAsync(v =>
            v.Titulo == videoJuegoRequest.Titulo && v.Plataforma == videoJuegoRequest.Plataforma);

        if (existeDuplicado)
        {
            return null;
        }

        var nuevoVideoJuego = new VideoJuego 
        { 
            Titulo = videoJuegoRequest.Titulo, 
            Genero = videoJuegoRequest.Genero, 
            Plataforma = videoJuegoRequest.Plataforma 
        };

        db.VideoJuego.Add(nuevoVideoJuego);
        await db.SaveChangesAsync();

        var nuevaCopia = new CopiaVideoJuego
        {
            VideoJuegoId = nuevoVideoJuego.Id,
            EstadoPrestado = false
        };

        db.CopiaVideoJuego.Add(nuevaCopia);
        await db.SaveChangesAsync();

        return new VideoJuegoResponseDto
        {
            Id = nuevoVideoJuego.Id,
            Nombre = nuevoVideoJuego.Titulo,
            Genero = nuevoVideoJuego.Genero,
            Plataforma = nuevoVideoJuego.Plataforma
        };
    }

    public async Task<VideoJuegoResponseDto?> GetVideoJuegoById(int id)
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

    public async Task<PagedResponseDto<VideoJuegoResponseDto>> Buscar(BusquedaVideoJuegoDto criterios)
    {
        var page = criterios.Page < 1 ? 1 : criterios.Page;
        var pageSize = criterios.PageSize < 1 ? 10 : criterios.PageSize;

        IQueryable<VideoJuego> query = db.VideoJuego.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(criterios.Titulo))
        {
            var titulo = criterios.Titulo.Trim();
            query = query.Where(vj => vj.Titulo == titulo);
        }

        if (!string.IsNullOrWhiteSpace(criterios.Plataforma))
        {
            var plataforma = criterios.Plataforma.Trim();
            query = query.Where(vj => vj.Plataforma == plataforma);
        }

        if (criterios.Generos is { Count: > 0 })
        {
            var generos = criterios.Generos
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Select(g => g.Trim())
                .ToList();

            if (generos.Count > 0)
            {
                query = query.Where(vj => generos.Contains(vj.Genero));
            }
        }

        query = (criterios.OrdenarPor?.ToLowerInvariant(), criterios.Descendente) switch
        {
            ("titulo", true) => query.OrderByDescending(vj => vj.Titulo),
            ("titulo", false) => query.OrderBy(vj => vj.Titulo),
            ("genero", true) => query.OrderByDescending(vj => vj.Genero),
            ("genero", false) => query.OrderBy(vj => vj.Genero),
            ("plataforma", true) => query.OrderByDescending(vj => vj.Plataforma),
            ("plataforma", false) => query.OrderBy(vj => vj.Plataforma),
            (_, true) => query.OrderByDescending(vj => vj.Id),
            _ => query.OrderBy(vj => vj.Id)
        };

        var totalItems = await query.CountAsync();
        var totalPages = totalItems == 0 ? 0 : (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(vj => new VideoJuegoResponseDto
            {
                Id = vj.Id,
                Nombre = vj.Titulo,
                Genero = vj.Genero,
                Plataforma = vj.Plataforma
            })
            .ToListAsync();

        return new PagedResponseDto<VideoJuegoResponseDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
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
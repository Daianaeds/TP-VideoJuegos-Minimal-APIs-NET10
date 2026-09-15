using BibliotecaVideojuegosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaVideojuegosApi.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VideoJuego> VideoJuego => Set<VideoJuego>();
    public DbSet<CopiaVideoJuego> CopiaVideoJuego => Set<CopiaVideoJuego>();
    public DbSet<PrestamoJuego> PrestamoVideoJuego => Set<PrestamoJuego>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<VideoJuego>().HasData(
            new VideoJuego { Id = 1,  Titulo = "The Legend of Zelda: Breath of the Wild", Genero = "Aventura",     Plataforma = "Switch" },
            new VideoJuego { Id = 2,  Titulo = "God of War",                              Genero = "Accion",       Plataforma = "PS4" },
            new VideoJuego { Id = 3,  Titulo = "God of War",                              Genero = "Accion",       Plataforma = "PC" },
            new VideoJuego { Id = 4,  Titulo = "The Witcher 3: Wild Hunt",                 Genero = "RPG",          Plataforma = "PC" },
            new VideoJuego { Id = 5,  Titulo = "The Witcher 3: Wild Hunt",                 Genero = "RPG",          Plataforma = "PS4" },
            new VideoJuego { Id = 6,  Titulo = "Red Dead Redemption 2",                    Genero = "Aventura",     Plataforma = "PS4" },
            new VideoJuego { Id = 7,  Titulo = "Red Dead Redemption 2",                    Genero = "Aventura",     Plataforma = "PC" },
            new VideoJuego { Id = 8,  Titulo = "Minecraft",                                Genero = "Sandbox",      Plataforma = "PC" },
            new VideoJuego { Id = 9,  Titulo = "Minecraft",                                Genero = "Sandbox",      Plataforma = "Switch" },
            new VideoJuego { Id = 10, Titulo = "Super Mario Odyssey",                      Genero = "Plataformas",  Plataforma = "Switch" },
            new VideoJuego { Id = 11, Titulo = "Elden Ring",                               Genero = "RPG",          Plataforma = "PS5" },
            new VideoJuego { Id = 12, Titulo = "Elden Ring",                               Genero = "RPG",          Plataforma = "PC" },
            new VideoJuego { Id = 13, Titulo = "FIFA 23",                                  Genero = "Deportes",     Plataforma = "PS5" },
            new VideoJuego { Id = 14, Titulo = "FIFA 23",                                  Genero = "Deportes",     Plataforma = "PC" },
            new VideoJuego { Id = 15, Titulo = "Animal Crossing: New Horizons",            Genero = "Simulacion",   Plataforma = "Switch" },
            new VideoJuego { Id = 16, Titulo = "Cyberpunk 2077",                           Genero = "RPG",          Plataforma = "PC" },
            new VideoJuego { Id = 17, Titulo = "Mario Kart 8",                             Genero = "Carreras",     Plataforma = "Switch" },
            new VideoJuego { Id = 18, Titulo = "Hollow Knight",                            Genero = "Metroidvania", Plataforma = "Switch" },
            new VideoJuego { Id = 19, Titulo = "Hollow Knight",                            Genero = "Metroidvania", Plataforma = "PC" },
            new VideoJuego { Id = 20, Titulo = "Dark Souls III",                           Genero = "RPG",          Plataforma = "PC" },
            new VideoJuego { Id = 21, Titulo = "Hades",                                    Genero = "Roguelike",    Plataforma = "Switch" },
            new VideoJuego { Id = 22, Titulo = "Persona 5",                                Genero = "RPG",          Plataforma = "PS4" },
            new VideoJuego { Id = 23, Titulo = "Grand Theft Auto V",                       Genero = "Accion",       Plataforma = "PS4" }
        );

        modelBuilder.Entity<CopiaVideoJuego>().HasData(
            new CopiaVideoJuego { Id = 1,  VideoJuegoId = 1,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 2,  VideoJuegoId = 2,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 3,  VideoJuegoId = 3,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 4,  VideoJuegoId = 4,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 5,  VideoJuegoId = 5,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 6,  VideoJuegoId = 6,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 7,  VideoJuegoId = 7,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 8,  VideoJuegoId = 8,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 9,  VideoJuegoId = 9,  EstadoPrestado = false },
            new CopiaVideoJuego { Id = 10, VideoJuegoId = 10, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 11, VideoJuegoId = 11, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 12, VideoJuegoId = 12, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 13, VideoJuegoId = 13, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 14, VideoJuegoId = 14, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 15, VideoJuegoId = 15, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 16, VideoJuegoId = 16, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 17, VideoJuegoId = 17, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 18, VideoJuegoId = 18, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 19, VideoJuegoId = 19, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 20, VideoJuegoId = 20, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 21, VideoJuegoId = 21, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 22, VideoJuegoId = 22, EstadoPrestado = false },
            new CopiaVideoJuego { Id = 23, VideoJuegoId = 23, EstadoPrestado = false }
        );
    }
}


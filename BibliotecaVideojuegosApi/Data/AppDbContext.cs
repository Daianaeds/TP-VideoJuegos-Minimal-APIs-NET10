using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaVideojuegosApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VideoJuego> VideoJuego => Set<VideoJuego>();
    public DbSet<CopiaVideoJuego> CopiaVideoJuego => Set<CopiaVideoJuego>();
    public DbSet<PrestamoJuego> PrestamoVideoJuego => Set<PrestamoJuego>();
}


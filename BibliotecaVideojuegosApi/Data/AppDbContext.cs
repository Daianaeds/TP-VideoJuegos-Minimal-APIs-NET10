using BibliotecaVideojuegosApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaVideojuegosApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VideoJuego> VideoJuegos => Set<VideoJuego>();
    public DbSet<CopiaVideoJuego> CopiasVideoJuegos => Set<CopiaVideoJuego>();
    public DbSet<PrestamoJuego> HistorialesPrestamos => Set<PrestamoJuego>();
}


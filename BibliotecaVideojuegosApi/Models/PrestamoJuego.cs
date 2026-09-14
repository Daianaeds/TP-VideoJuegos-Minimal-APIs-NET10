namespace BibliotecaVideojuegosApi.Models;

public class PrestamoJuego
{
    public int Id { get; set; }
    public int CopiaVideoJuegoId { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public string NombreUsuario { get; set; } = null!;
}
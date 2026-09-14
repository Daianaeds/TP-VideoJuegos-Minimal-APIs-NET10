namespace BibliotecaVideojuegosApi.Models;

public class CopiaVideoJuego
{
    public int Id { get; set; }
    public int VideoJuegoId { get; set; }
    public bool EstadoPrestado { get; set; }
}
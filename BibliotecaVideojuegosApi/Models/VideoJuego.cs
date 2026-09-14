namespace BibliotecaVideojuegosApi.Models;

public class VideoJuego
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Genero { get; set; }
    public required string Plataforma { get; set; }
}
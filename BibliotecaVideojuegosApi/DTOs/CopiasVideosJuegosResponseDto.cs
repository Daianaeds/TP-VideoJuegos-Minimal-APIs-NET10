namespace BibliotecaVideojuegosApi.DTOs;

public class CopiasVideosJuegosResponseDto
{
    public int Id { get; set; }
    public int VideoJuegoId { get; set; }
    public bool EstadoPrestado { get; set; }
    public VideoJuegoResponseDto? VideoJuego { get; set; }
}
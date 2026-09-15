namespace BibliotecaVideojuegosApi.DTOs;

public class BusquedaVideoJuegoDto
{
    public List<string>? Generos { get; set; }
    public string? Plataforma { get; set; }
    public string? Titulo { get; set; }
    public string? OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

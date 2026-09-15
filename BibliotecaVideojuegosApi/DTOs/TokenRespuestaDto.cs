namespace BibliotecaVideojuegosApi.DTOs;

public class TokenRespuestaDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracion { get; set; }
}

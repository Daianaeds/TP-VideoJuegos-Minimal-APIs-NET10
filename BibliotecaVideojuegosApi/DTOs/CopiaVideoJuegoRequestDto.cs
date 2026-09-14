using System.ComponentModel.DataAnnotations;
namespace BibliotecaVideojuegosApi.DTOs;

public class CopiaVideoJuegoRequestDto
{
    [Required(ErrorMessage = "El id del video juego es obligatorio.")]
    public required int VideoJuegoId { get; set; }
    public bool EstadoPrestado { get; set; } = false;
}
using System.ComponentModel.DataAnnotations;
namespace BibliotecaVideojuegosApi.DTOs;

public class VideoJuegoRequestDto
{
    [Required(ErrorMessage = "El titulo es obligatorio.")]
    [MinLength(2, ErrorMessage = "El titulo debe tener al menos 2 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El género es obligatorio.")]
    [MinLength(2, ErrorMessage = "El género debe tener al menos 2 caracteres.")]
    public string Genero { get; set; } = string.Empty;

    [Required(ErrorMessage = "La plataforma es obligatoria.")]
    [MinLength(2, ErrorMessage = "La plataforma debe tener al menos 2 caracteres.")]
    public string Plataforma { get; set; } = string.Empty;
}
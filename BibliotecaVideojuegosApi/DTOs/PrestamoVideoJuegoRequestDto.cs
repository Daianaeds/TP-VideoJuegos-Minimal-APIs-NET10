using System.ComponentModel.DataAnnotations;
namespace BibliotecaVideojuegosApi.DTOs;

public class PrestamoVideoJuegoRequestDto
{
    [Required(ErrorMessage = "El id de la copia del video juego es obligatorio.")]
    public int CopiaVideoJuegoId { get; set; }
    [Required(ErrorMessage = "La fecha de préstamo es obligatoria.")]
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    [Required(ErrorMessage = "El nombre del usuario al que se le presta es obligatorio.")]
    public string NombreUsuario { get; set; } = null!;
}
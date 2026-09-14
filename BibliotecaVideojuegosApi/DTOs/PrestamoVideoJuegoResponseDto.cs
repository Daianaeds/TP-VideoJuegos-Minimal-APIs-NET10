using BibliotecaVideojuegosApi.Models;
using System.ComponentModel.DataAnnotations;
namespace BibliotecaVideojuegosApi.DTOs;

public class PrestamoVideoJuegoResponseDto
{
    public int CopiaVideoJuegoId { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public string NombreUsuario { get; set; } = null!;
}
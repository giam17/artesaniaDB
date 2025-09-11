using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcTejerina.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar {1} caracteres")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    public string Nombre { get; set; } = default!;

    [EmailAddress(ErrorMessage = "Formato de correo inválido")]
    [StringLength(120, ErrorMessage = "El email no puede superar {1} caracteres")]
    public string? Email { get; set; }

    [StringLength(200, ErrorMessage = "La dirección no puede superar {1} caracteres")]
    public string? Direccion { get; set; }

    public ICollection<Pedido>? Pedidos { get; set; }
}

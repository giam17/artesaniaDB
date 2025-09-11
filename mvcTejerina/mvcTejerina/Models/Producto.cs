using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcTejerina.Models;

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(120, ErrorMessage = "El nombre no puede superar {1} caracteres")]
    public string Nombre { get; set; } = default!;

    [Range(0.01, 999_999, ErrorMessage = "El precio debe ser mayor a 0")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioUnitario { get; set; }

    public ICollection<DetallePedido>? Detalles { get; set; }
}

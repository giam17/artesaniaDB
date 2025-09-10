using System.ComponentModel.DataAnnotations;

namespace mvcTejerina.Models;

public class Producto
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nombre { get; set; } = default!;

    [Range(0, 999999)]
    public decimal PrecioUnitario { get; set; }   

    public ICollection<DetallePedido>? Detalles { get; set; }
}

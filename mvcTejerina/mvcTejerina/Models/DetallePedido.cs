using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcTejerina.Models;

public class DetallePedido : IValidatableObject
{
    public int Id { get; set; }

    [Display(Name = "Pedido")]
    [Required(ErrorMessage = "Debe seleccionar un pedido")]
    public int IdPedido { get; set; }
    public Pedido? Pedido { get; set; }

    [Display(Name = "Producto")]
    [Required(ErrorMessage = "Debe seleccionar un producto")]
    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }

    [Range(1, 100000, ErrorMessage = "La cantidad debe estar entre 1 y 100000")]
    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(typeof(decimal), "0.01", "999999", ErrorMessage = "El precio unitario debe ser mayor a 0")]
    public decimal PrecioUnitario { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Cantidad <= 0)
            yield return new ValidationResult("La cantidad debe ser mayor a 0", new[] { nameof(Cantidad) });

        if (PrecioUnitario <= 0)
            yield return new ValidationResult("El precio unitario debe ser mayor a 0", new[] { nameof(PrecioUnitario) });
    }
}

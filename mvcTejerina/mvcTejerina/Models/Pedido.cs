using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcTejerina.Models;

public class Pedido : IValidatableObject
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha del pedido")]
    public DateTime FechaPedido { get; set; } = DateTime.Now;

    [Display(Name = "Cliente")]
    [Required(ErrorMessage = "Debe seleccionar un cliente")]
    public int IdCliente { get; set; }
    public Cliente? Cliente { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    [StringLength(20, ErrorMessage = "El estado no puede superar {1} caracteres")]
    public string Estado { get; set; } = "Pendiente";

    [Column(TypeName = "decimal(18,2)")]
    public decimal MontoTotal { get; set; }

    public ICollection<DetallePedido>? Detalles { get; set; }

    public static class EstadosPedido
    {
        public static readonly string[] Permitidos = new[] { "Pendiente", "Enviado", "Entregado" };
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!EstadosPedido.Permitidos.Contains(Estado))
            yield return new ValidationResult("Estado no permitido", new[] { nameof(Estado) });

    }
}

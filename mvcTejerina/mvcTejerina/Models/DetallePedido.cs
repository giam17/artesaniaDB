using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcTejerina.Models;

public class DetallePedido
{
    public int Id { get; set; }

    [Display(Name = "Pedido")]
    public int IdPedido { get; set; }
    public Pedido? Pedido { get; set; }

    [Display(Name = "Producto")]
    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }

    [Range(1, 100000)]
    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioUnitario { get; set; }  // copia del precio del producto al momento del pedido
}

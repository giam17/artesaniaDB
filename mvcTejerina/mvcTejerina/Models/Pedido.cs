using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcTejerina.Models;

public class Pedido
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaPedido { get; set; } = DateTime.Now;

    [Display(Name = "Cliente")]
    public int IdCliente { get; set; }
    public Cliente? Cliente { get; set; }

    // Según tu requerimiento: string ("Pendiente", "Enviado", "Entregado")
    [Required, StringLength(20)]
    public string Estado { get; set; } = "Pendiente";

    [Column(TypeName = "decimal(18,2)")]
    public decimal MontoTotal { get; set; }  // opcionalmente puedes calcularlo al guardar
    public ICollection<DetallePedido>? Detalles { get; set; }
    public static class EstadosPedido
    {
        public static readonly string[] Permitidos = new[] { "Pendiente", "Enviado", "Entregado" };
    }

}

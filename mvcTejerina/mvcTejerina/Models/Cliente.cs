using System.ComponentModel.DataAnnotations;

namespace mvcTejerina.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = default!;

    [EmailAddress, StringLength(120)]
    public string? Email { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    // Navegación
    public ICollection<Pedido>? Pedidos { get; set; }
}

using Microsoft.EntityFrameworkCore;
using mvcTejerina.Data;

namespace mvcTejerina.Services;

public class PedidoTotalsService
{
    private readonly ArtesanosDbContext _context;

    public PedidoTotalsService(ArtesanosDbContext context)
    {
        _context = context;
    }

    /// Copia el precio actual del producto (catálogo) para un detalle
    public async Task<decimal> GetPrecioProductoAsync(int idProducto)
    {
        var precio = await _context.Productos
            .Where(p => p.Id == idProducto)
            .Select(p => p.PrecioUnitario)
            .FirstOrDefaultAsync();

        return precio;
    }

    /// Recalcula y persiste el MontoTotal del pedido
    public async Task RecalcularMontoTotalAsync(int pedidoId)
    {
        var total = await _context.DetallesPedido
            .Where(d => d.IdPedido == pedidoId)
            .SumAsync(d => (decimal?)d.Cantidad * d.PrecioUnitario) ?? 0m;

        var pedido = await _context.Pedidos.FindAsync(pedidoId);
        if (pedido != null)
        {
            pedido.MontoTotal = total;
            await _context.SaveChangesAsync();
        }
    }
}

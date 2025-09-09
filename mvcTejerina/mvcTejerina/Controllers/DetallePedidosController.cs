using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvcTejerina.Data;
using mvcTejerina.Models;
using mvcTejerina.Services;

namespace mvcTejerina.Controllers
{
    public class DetallePedidosController : Controller
    {
        private readonly ArtesanosDbContext _context;
        private readonly PedidoTotalsService _totals;

        public DetallePedidosController(ArtesanosDbContext context, PedidoTotalsService totals)
        {
            _context = context;
            _totals = totals;
        }

        // GET: DetallePedidos
        public async Task<IActionResult> Index()
        {
            var detalles = _context.DetallesPedido
                .Include(d => d.Pedido)
                .Include(d => d.Producto);
            return View(await detalles.ToListAsync());
        }

        // GET: DetallePedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var detallePedido = await _context.DetallesPedido
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detallePedido == null) return NotFound();
            return View(detallePedido);
        }

        // GET: DetallePedidos/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View();
        }

        // POST: DetallePedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdProducto,Cantidad")] DetallePedido detallePedido)
        {
            // Copiamos precio del producto SIEMPRE
            detallePedido.PrecioUnitario = await _totals.GetPrecioProductoAsync(detallePedido.IdProducto);

            if (ModelState.IsValid)
            {
                _context.Add(detallePedido);
                await _context.SaveChangesAsync();

                await _totals.RecalcularMontoTotalAsync(detallePedido.IdPedido);
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var detallePedido = await _context.DetallesPedido.FindAsync(id);
            if (detallePedido == null) return NotFound();

            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // POST: DetallePedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdProducto,Cantidad")] DetallePedido detallePedido)
        {
            if (id != detallePedido.Id) return NotFound();

            // Actualiza precio por si cambió el producto
            detallePedido.PrecioUnitario = await _totals.GetPrecioProductoAsync(detallePedido.IdProducto);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedido);
                    await _context.SaveChangesAsync();

                    await _totals.RecalcularMontoTotalAsync(detallePedido.IdPedido);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DetallesPedido.Any(e => e.Id == detallePedido.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var detallePedido = await _context.DetallesPedido
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detallePedido == null) return NotFound();
            return View(detallePedido);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);
            if (detalle != null)
            {
                int pedidoId = detalle.IdPedido;

                _context.DetallesPedido.Remove(detalle);
                await _context.SaveChangesAsync();

                await _totals.RecalcularMontoTotalAsync(pedidoId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

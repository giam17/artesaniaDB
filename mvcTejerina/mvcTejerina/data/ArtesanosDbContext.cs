using Microsoft.EntityFrameworkCore;
using mvcTejerina.Models;

namespace mvcTejerina.Data;

public class ArtesanosDbContext : DbContext
{
    public ArtesanosDbContext(DbContextOptions<ArtesanosDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relaciones
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.IdCliente)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DetallePedido>()
            .HasOne(d => d.Pedido)
            .WithMany(p => p.Detalles)
            .HasForeignKey(d => d.IdPedido)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DetallePedido>()
            .HasOne(d => d.Producto)
            .WithMany(p => p.Detalles)
            .HasForeignKey(d => d.IdProducto)
            .OnDelete(DeleteBehavior.Restrict);

        // Precisión decimales
        modelBuilder.Entity<Pedido>().Property(p => p.MontoTotal).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<DetallePedido>().Property(p => p.PrecioUnitario).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Producto>().Property(p => p.PrecioUnitario).HasColumnType("decimal(18,2)");
    }
}

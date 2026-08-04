// Data/InventarioContext.cs
using Microsoft.EntityFrameworkCore;
using InventarioWeb.Models;
 
namespace InventarioWeb.Data
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options)
            : base(options) { }
 
        // Cada DbSet<T> es una tabla
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
    }
}

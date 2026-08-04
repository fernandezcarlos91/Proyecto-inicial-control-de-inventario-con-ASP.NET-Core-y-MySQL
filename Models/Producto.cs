// Models/Producto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace InventarioWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }
 
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;
 
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 10_000_000, ErrorMessage = "El precio debe ser positivo.")]
        public decimal Precio { get; set; }
 
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
 
        public DateTime CreadoEn { get; set; } = DateTime.Now;
 
        // Clave foránea hacia Categoria (equivalente a categoria_id)
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}

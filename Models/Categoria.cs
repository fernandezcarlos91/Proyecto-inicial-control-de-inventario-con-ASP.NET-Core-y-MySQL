// Models/Categoria.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventarioWeb.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(80)]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        // Se sigue usando para el JOIN en EF Core, pero ya no viaja en el JSON
        [JsonIgnore]
        public List<Producto> Productos { get; set; } = new();
    }
}
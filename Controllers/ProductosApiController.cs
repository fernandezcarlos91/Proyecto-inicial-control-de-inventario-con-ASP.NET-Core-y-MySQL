// Controllers/ProductosApiController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioWeb.Data;
using InventarioWeb.Models;
 
namespace InventarioWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // → api/ProductosApi
    public class ProductosApiController : ControllerBase
    {
        private readonly InventarioContext _context;
        public ProductosApiController(InventarioContext context) => _context = context;
 
        // GET api/productosapi
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? stockMinimo)
        {
            var query = _context.Productos.Include(p => p.Categoria).AsQueryable();
 
            if (stockMinimo.HasValue)
            {
                query = query.Where(p => p.Stock <= stockMinimo.Value);
            }
 
            var productos = await query.OrderBy(p => p.Nombre).ToListAsync();
            return Ok(new { total = productos.Count, productos });
        }

 
        // GET api/productosapi/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound(new { error = "Producto no encontrado", id });
            return Ok(producto);
        }
 
        // POST api/productosapi
        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }
 
        // PUT api/productosapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        // DELETE api/productosapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET api/productosapi/resumen
        [HttpGet("resumen")]
        public async Task<IActionResult> GetResumen()
        {
            var productos = await _context.Productos.ToListAsync();
 
            var resumen = new
            {
                totalProductos = productos.Count,
                valorTotalInventario = productos.Sum(prod => prod.Precio * prod.Stock),
                productosSinStock = productos.Count(prod => prod.Stock == 0),
                productosStockBajo = productos.Count(prod => prod.Stock > 0 && prod.Stock <= 5)
            };
 
            return Ok(resumen);
        }

    }
}

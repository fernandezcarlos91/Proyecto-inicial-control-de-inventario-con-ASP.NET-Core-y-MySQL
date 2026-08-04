// Controllers/CategoriasApiController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioWeb.Data;
using InventarioWeb.Models;

namespace InventarioWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // → api/categoriasapi
    public class CategoriasApiController : ControllerBase
    {
        private readonly InventarioContext _context;
        public CategoriasApiController(InventarioContext context) => _context = context;

        // GET api/categoriasapi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(new { total = categorias.Count, categorias });
        }

        // GET api/categoriasapi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound(new { error = "Categoría no encontrada", id });
            return Ok(categoria);
        }

        // POST api/categoriasapi
        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        // PUT api/categoriasapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Categoria categoria)
        {
            if (id != categoria.Id) return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/categoriasapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
// Controllers/CategoriasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioWeb.Data;
using InventarioWeb.Models;

namespace InventarioWeb.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly InventarioContext _context;
        public CategoriasController(InventarioContext context) => _context = context;

        // GET /Categorias
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias
                .Include(c => c.Productos)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
            return View(categorias);
        }

        // GET /Categorias/Create
        public IActionResult Create() => View();

        // POST /Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Categoría creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Categorias/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // POST /Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id) return NotFound();
            if (!ModelState.IsValid) return View(categoria);

            _context.Update(categoria);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Categoría actualizada.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Categorias/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // POST /Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null) return NotFound();

            // Regla de negocio: no se elimina una categoría con productos asociados
            if (categoria.Productos.Any())
            {
                TempData["Error"] =
                    $"No se puede eliminar '{categoria.Nombre}' porque tiene {categoria.Productos.Count} producto(s) asociado(s). Reasígnalos o elimínalos primero.";
                return RedirectToAction(nameof(Index));
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Categoría eliminada.";
            return RedirectToAction(nameof(Index));
        }
    }
}
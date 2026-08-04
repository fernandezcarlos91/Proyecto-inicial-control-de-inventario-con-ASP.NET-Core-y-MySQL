using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioWeb.Data;
using InventarioWeb.Models;

namespace InventarioWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly InventarioContext _context;

        public ProductosController(InventarioContext context)
        {
            _context = context;
        }

        // GET /Productos
        // GET /Productos?categoriaId=2&busqueda=teclado
        public async Task<IActionResult> Index(int? categoriaId, string? busqueda)
        {
            var query = _context.Productos.Include(p => p.Categoria).AsQueryable();
        
            if (categoriaId.HasValue)
            {
                query = query.Where(p => p.CategoriaId == categoriaId.Value);
            }
        
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                query = query.Where(p => p.Nombre.Contains(busqueda));
            }
        
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            ViewBag.CategoriaSeleccionada = categoriaId;
            ViewBag.Busqueda = busqueda;
        
            var productos = await query.OrderBy(p => p.Nombre).ToListAsync();
            return View(productos);
        }


        // GET /Productos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();
            return View(producto);
        }

        // GET /Productos/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        // POST /Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Producto creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Productos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        // POST /Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();
            if (!ModelState.IsValid) return View(producto);

            _context.Update(producto);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Producto actualizado.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Productos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        // POST /Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            TempData["Mensaje"] = "Producto eliminado.";
            return RedirectToAction(nameof(Index));
        }
    } // <-- cierre de la CLASE (una sola vez, al final de todos los métodos)
} // <-- cierre del NAMESPACE (una sola vez, al final del archivo)
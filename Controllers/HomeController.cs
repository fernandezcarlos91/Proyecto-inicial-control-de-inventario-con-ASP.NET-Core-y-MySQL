// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioWeb.Data;
 
namespace InventarioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly InventarioContext _context;
        public HomeController(InventarioContext context) => _context = context;
 
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos.ToListAsync();
 
            ViewBag.TotalProductos = productos.Count;
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();
            ViewBag.ValorInventario = productos.Sum(p => p.Precio * p.Stock);
            ViewBag.SinStock = productos.Count(p => p.Stock == 0);
 
            return View();
        }
    }
}

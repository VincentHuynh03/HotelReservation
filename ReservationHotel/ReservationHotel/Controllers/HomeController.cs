using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationHotel.Models;
using System.Diagnostics;

namespace ReservationHotel.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chambres
        public async Task<IActionResult> Index()
        {
            return _context.Chambres != null ?
                        View(await _context.Chambres.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Chambres'  is null.");
        }

        // GET: Chambres/Réserver/5
        public async Task<IActionResult> Réserver(int? id)
        {
            if (id == null || _context.Chambres == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambres
                .FirstOrDefaultAsync(m => m.ChambreId == id);
            if (chambre == null)
            {
                return NotFound();
            }

            return View(chambre);
        }

        public IActionResult ServiceClientèle()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
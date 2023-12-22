using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationHotel.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index(int? maxPeople, DateTime? checkin, DateTime? checkout)
        {
            IQueryable<Chambre> chambresQuery = _context.Chambres;

            if (maxPeople.HasValue)
            {
                chambresQuery = chambresQuery.Where(c => c.MaxPersonne >= maxPeople.Value);
            }

            if (checkin.HasValue)
            {
                chambresQuery = chambresQuery.Where(c =>
                    c.CheckIn == null || checkin >= c.CheckOut || checkin <= c.CheckIn
                );
            }

            if (checkout.HasValue)
            {
                chambresQuery = chambresQuery.Where(c =>
                    c.CheckOut == null || checkout <= c.CheckIn || checkout >= c.CheckOut
                );
            }



            var chambres = await chambresQuery.ToListAsync();

            return View(chambres);
        }


        // GET: Chambres/Réserver/5 Task<IActionResult>
        public async Task<IActionResult> Réserver(int id)
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

            var viewModel = new ReservationViewModel
            {
                Chambre = chambre,
                Reservation = new Reservation
                {
                    ChambreId = id,
                    Annuler = false,
                    UserId = 1,
                }
            };
            ViewData["ChambreId"] = new SelectList(_context.Chambres, "ChambreId", "Description");
            ViewData["UserId"] = new SelectList(_context.Utilisateurs, "UserId", "Email");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Réserver(ReservationViewModel viewModel)
        {
            _context.Reservations.Add(viewModel.Reservation);
            _context.SaveChanges();
            return RedirectToAction("Index"); // Modifiez selon vos besoins
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
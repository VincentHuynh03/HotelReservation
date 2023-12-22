using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationHotel.Models;

namespace ReservationHotel.Controllers
{
    public class ChambresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChambresController(ApplicationDbContext context)
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

        // GET: Chambres/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Chambres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chambres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChambreId,NumeroChambre,Description,TypeChambre,QuantitePersonnes,Prix,MaxPersonne,Vue,Lit,Photo,CheckIn,CheckOut")] Chambre chambre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chambre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chambre);
        }

        // GET: Chambres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chambres == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre == null)
            {
                return NotFound();
            }
            return View(chambre);
        }

        // POST: Chambres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChambreId,NumeroChambre,Description,TypeChambre,QuantitePersonnes,Prix,MaxPersonne,Vue,Lit,Photo,CheckIn,CheckOut")] Chambre chambre)
        {
            if (id != chambre.ChambreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chambre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChambreExists(chambre.ChambreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chambre);
        }

        // GET: Chambres/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Chambres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chambres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Chambres'  is null.");
            }
            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre != null)
            {
                _context.Chambres.Remove(chambre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChambreExists(int id)
        {
          return (_context.Chambres?.Any(e => e.ChambreId == id)).GetValueOrDefault();
        }
    }
}

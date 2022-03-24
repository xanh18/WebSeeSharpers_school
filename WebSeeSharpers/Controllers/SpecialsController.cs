#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Controllers
{
    public class SpecialsController : Controller
    {
        private readonly WebSeeSharpersContext _context;

        public SpecialsController(WebSeeSharpersContext context)
        {
            _context = context;
        }

        // GET: Specials
        public async Task<IActionResult> Index()
        {
            var specials = _context.Special.Where(s =>
                s.BeginDate < DateTime.Today && s.EndDate > (DateTime.Now));

            return View(await specials.ToListAsync());
        }

        // GET: Specials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = await _context.Special
                .FirstOrDefaultAsync(m => m.Id == id);
            if (special == null)
            {
                return NotFound();
            }

            return View(special);
        }

        // GET: Specials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BeginDate,EndDate,Cost,Description")] Special special)
        {
            if (ModelState.IsValid)
            {
                _context.Add(special);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(special);
        }

        // GET: Specials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = await _context.Special.FindAsync(id);
            if (special == null)
            {
                return NotFound();
            }
            return View(special);
        }

        // POST: Specials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BeginDate,EndDate,Cost,Description")] Special special)
        {
            if (id != special.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(special);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialExists(special.Id))
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
            return View(special);
        }

        // GET: Specials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = await _context.Special
                .FirstOrDefaultAsync(m => m.Id == id);
            if (special == null)
            {
                return NotFound();
            }

            return View(special);
        }

        // POST: Specials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var special = await _context.Special.FindAsync(id);
            _context.Special.Remove(special);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialExists(int id)
        {
            return _context.Special.Any(e => e.Id == id);
        }
    }
}

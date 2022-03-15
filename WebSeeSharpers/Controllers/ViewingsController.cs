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
    public class ViewingsController : Controller
    {
        private readonly WebSeeSharpersContext _context;

        public ViewingsController(WebSeeSharpersContext context)
        {
            _context = context;
        }

        // GET: Viewings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Viewings
                .Include(M => M.Movie)
                .Include(T => T.Theatre)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Viewings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .Include(M => M.Movie).Include(T => T.Theatre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewing == null)
            {
                return NotFound();
            }

            return View(viewing);
        }

        // GET: Viewings/Create
        public IActionResult Create()
        {
            MovieDropDownList();
            return View();
        }

        // POST: Viewings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDateTimem,MovieId")] Viewing viewing)
        {
            if (ModelState.IsValid)
            {   
                _context.Add(viewing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            MovieDropDownList(viewing.MovieID);
            return View(viewing);
        }

        // GET: Viewings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .Include(m => m.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
            if (viewing == null)
            {
                return NotFound();
            }
            MovieDropDownList(viewing.MovieID);
            return View(viewing);
        }

        // POST: Viewings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewingToUpdate = await _context.Viewings
                .Include(m => m.MovieID)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (await TryUpdateModelAsync<Viewing>(viewingToUpdate,
                "",
                 m => m.Id))

                if (ModelState.IsValid)
            {
                try
                {
                  
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                return RedirectToAction(nameof(Index));
            }
            MovieDropDownList(viewingToUpdate.MovieID);
            return View(viewingToUpdate);
        }

        private void MovieDropDownList(object selectedMovie = null)
        {
            var moviesQuery = from m in _context.Movie
                                   orderby m.Title
                                   select m;
            ViewBag.MovieId = new SelectList(moviesQuery.AsNoTracking(), "MovieId", "Title", selectedMovie);
        }

        // GET: Viewings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewing == null)
            {
                return NotFound();
            }

            return View(viewing);
        }

        // POST: Viewings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewing = await _context.Viewings.FindAsync(id);
            _context.Viewings.Remove(viewing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewingExists(int id)
        {
            return _context.Viewings.Any(e => e.Id == id);
        }
    }
}

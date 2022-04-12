using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Controllers
{
    [Authorize(Roles = "Admin, BioscoopManager,Backoffice")]
    public class ViewingsController : Controller
    {
        private readonly WebSeeSharpersContext _context;

        public ViewingsController(WebSeeSharpersContext context, ILogger<ViewingsController> logger)
        {
            _context = context;
        }

        // GET: Viewings
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewings = _context.Viewings.Where(v =>
                v.StartDateTime > DateTime.Today && v.StartDateTime < (DateTime.Now.AddDays(7)));

            return View(await viewings
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
            TheatreDropDownList();

            return View();
        }

        // POST: Viewings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDateTime,MovieId,TheatreId")] Viewing viewing)
        {

            ModelState.Remove(nameof(Viewing.Movie));
            ModelState.Remove(nameof(Viewing.Theatre));
            ModelState.Remove(nameof(Viewing.ViewingSeats));


            if (ModelState.IsValid)
            {
                _context.Add(viewing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            MovieDropDownList(viewing.MovieId);
            TheatreDropDownList(viewing.TheatreId);

            return View(viewing);
        }

        // GET: Viewings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .Include(m => m.Movie)
                .Include(t => t.Theatre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m=> m.Id == id);
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
            Viewing viewing = await _context.Viewings
               .Include(m => m.Movie)
               .Include(t => t.Theatre)
               .SingleAsync(i => i.Id == id);

            _context.Viewings.Remove(viewing);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewingExists(int id)
        {
            return _context.Viewings.Any(e => e.Id == id);
        }

        private void MovieDropDownList(object selectedMovie = null)
        {
            var movieQuery = from m in _context.Movie
                                   orderby m.Title
                                   select m;
            ViewBag.MovieId = new SelectList(movieQuery.AsNoTracking(), nameof(Movie.Id), nameof(Movie.Title), selectedMovie);
        }

        private void TheatreDropDownList(object selectedTheatre = null)
        {
            var theatreQuery = from t in _context.Theatres
                                   orderby t.Id
                                   select t;
            ViewBag.TheatreId = new SelectList(theatreQuery.AsNoTracking(), nameof(Theatre.Id), nameof(Theatre.Id), selectedTheatre);
        }



    }
}
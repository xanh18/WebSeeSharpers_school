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

        public ViewingsController(WebSeeSharpersContext context, ILogger<ViewingsController> logger)
        {
            _context = context;
  
        }

        // GET: Viewings
        public async Task<IActionResult> Index()
        {

            var viewings = from v in _context.Viewings
                           select v;
            viewings = viewings.Where(v => v.StartDateTime > DateTime.Now && v.StartDateTime < (DateTime.Now.AddDays(7)));

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
            return View();
        }

        // POST: Viewings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDateTime")] Viewing viewing)
        {
            if (ModelState.IsValid)
            {   
                _context.Add(viewing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewing);
        }

        // GET: Viewings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings.FindAsync(id);
            if (viewing == null)
            {
                return NotFound();

                
            }
            return View(viewing);
        }

        // POST: Viewings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,starttime")] Viewing viewing)
        {
                if (id != viewing.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(viewing);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ViewingExists(viewing.Id))
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
                return View(viewing);
        }
            // GET: Viewings/Delete/5
        public async Task<IActionResult> Delete(int? id) { 
        
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

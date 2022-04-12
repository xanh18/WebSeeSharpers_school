#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin, Bioscoopmanager, Kassamedewerker")]
    public class LostItemsController : Controller
    {
        private readonly WebSeeSharpersContext _context;

        public LostItemsController(WebSeeSharpersContext context)
        {
            _context = context;
        }

        // GET: LostItems
        [AllowAnonymous]
         public async Task<IActionResult> Index()
        {
            return View(await _context.LostItems.ToListAsync());
        }

        // GET: LostItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }

        // GET: LostItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LostItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Description,TimeFound,Picture")] LostItem lostItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lostItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lostItem);
        }

        // GET: LostItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItems.FindAsync(id);
            if (lostItem == null)
            {
                return NotFound();
            }
            return View(lostItem);
        }

        // POST: LostItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Item,Description,TimeFound,Picture")] LostItem lostItem)
        {
            if (id != lostItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lostItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostItemExists(lostItem.Id))
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
            return View(lostItem);
        }

        // GET: LostItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }

        // POST: LostItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lostItem = await _context.LostItems.FindAsync(id);
            _context.LostItems.Remove(lostItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostItemExists(int id)
        {
            return _context.LostItems.Any(e => e.Id == id);
        }
    }
}

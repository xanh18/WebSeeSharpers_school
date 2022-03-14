using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;
using WebSeeSharpers.Services.SeatService;

namespace WebSeeSharpers.Controllers
{
    public class SeatSelectionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebSeeSharpersContext _context;

        public SeatSelectionController(WebSeeSharpersContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            Viewing? viewing = _context.Viewings
                .Include(v => v.Theatre)
                .Include(v => v.ViewingSeats)
                .First(s => s.Id == 2);

            if (viewing == null)
            {
                Debug.WriteLine("No viewing found!");
                return Redirect("/");
            }

            var service = new SeatService(viewing, _context);
            service.OccupyNextSeat(20);

            var rowList = service.GetSeatsOrderedByNumber();

            Debug.WriteLine("- - - - - - - -[Start]- - - - - - - -");
            rowList.ForEach(r => { r.ForEach(s => Debug.WriteLine($"Rij: {s.RowNumber} Stoel: {s.Number}")); });
            Debug.WriteLine("- - - - - - - -[End]- - - - - - - -");

            return View(rowList);
        }
    }
}
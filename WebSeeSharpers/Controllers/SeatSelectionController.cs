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
        private static SeatService _seatService;
        private static List<List<Seat>> _rowList;

        public SeatSelectionController(WebSeeSharpersContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            Viewing? viewing;
            try
            {
                viewing = GetViewing(2);
            }
            catch (Exception e)
            {
                return Redirect("/");
            }

            if (viewing == null) return Redirect("/");

            _seatService = new SeatService(viewing, _context);
            _seatService.OccupyNextSeat(7);
            _rowList = _seatService.GetSeatsOrderedByNumber();

            return View(_rowList);
        }

        public void ReserveSeat_click(string seatX)
        {

            Debug.WriteLine(seatX);
        }

        private Viewing? GetViewing(int id)
        {
            try
            {
                return _context.Viewings
                    .Include(v => v.Theatre)
                    .Include(v => v.ViewingSeats)
                    .First(s => s.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
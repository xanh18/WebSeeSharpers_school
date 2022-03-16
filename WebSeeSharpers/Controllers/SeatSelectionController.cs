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

        public IActionResult Index(int viewingId)
        {
            Viewing? viewing;
            try
            {
                viewing = GetViewing(viewingId);
            }
            catch (Exception e)
            {
                return Redirect("/Viewings");
            }

            if (viewing == null) return Redirect("/Viewings");

            _seatService = new SeatService(viewing, _context);
            var reservatedSeats = _seatService.OccupyNextSeat(1);
            _rowList = _seatService.GetSeatsOrderedByNumber();

            return View(_rowList);
        }

        public IActionResult save(int viewingId)
        {
            Viewing? viewing;
            try
            {
                viewing = GetViewing(viewingId);
            }
            catch (Exception e)
            {
                return Redirect("/Viewings");
            }

            if (viewing == null) return Redirect("/Viewings");

            _seatService = new SeatService(viewing, _context);
            var selectedSeats = _seatService.OccupyNextSeat(1);

            return RedirectToAction("show", "Order", new { reservedSeats = SerializeSeatsToXAndYString(selectedSeats), viewingId = viewing.Id });
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

        private string SerializeSeatsToXAndYString(List<Seat> seats)
        {
            string seatXAndYs = string.Empty;

            seats.ForEach(s =>
            {
                seatXAndYs += $"{(int) s.Position.X}_{(int) s.Position.Y};";
            });

            return seatXAndYs;
        }
    }
}
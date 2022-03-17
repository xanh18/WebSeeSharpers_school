using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Data;
using WebSeeSharpers.Helpers;
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

            if (viewing == null)
                return Redirect("/Viewings");

            SeatService seatService = new(viewing, _context);
            ViewBag.Rows = seatService.GetSeatsOrderedByNumber();

            return View(viewing);
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

            if (viewing == null)
                return Redirect("/Viewings");

            SeatService _seatService = new(viewing, _context);
            var selectedSeats = _seatService.OccupyNextSeat(1);

            return RedirectToAction("show", "Order",
                new {reservedSeats = SeatPositionHelper.SerializeSeatToString(selectedSeats), viewingId = viewing.Id});
        }

        public IActionResult SaveCustom(string seatPosition, int viewingId)
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

            if (viewing == null)
                return Redirect("/Viewings");
            
            SeatService seatService = new(viewing, _context);
            var positions = SeatPositionHelper.DeserializePositionToVector2List(seatPosition);

            positions.ForEach(p => seatService.OccupySeat(p));

            return RedirectToAction("show", "Order",
                new {reservedSeats = SeatPositionHelper.SerializePositionToString(positions), viewingId = viewing.Id});
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
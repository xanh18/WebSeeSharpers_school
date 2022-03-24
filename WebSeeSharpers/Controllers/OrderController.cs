using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Data;
using WebSeeSharpers.Helpers;
using WebSeeSharpers.Models;
using WebSeeSharpers.Services.SeatService;

namespace WebSeeSharpers.Controllers;

public class OrderController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WebSeeSharpersContext _context;

    public OrderController(WebSeeSharpersContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult show(string reservedSeats, int viewingId)
    {
        Viewing? viewing;
        try
        {
            viewing = GetViewing(viewingId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Redirect("/Viewings");
        }

        if (viewing == null)
            return Redirect("/Viewings");

        SeatService service = new(viewing, _context);

        var seatPositions = SeatPositionHelper.DeserializePositionToVector2List(reservedSeats);

        ViewBag.Seats = service.GetSeats(seatPositions).ToArray();
        return View("show", viewing);
    }

    private Viewing? GetViewing(int id)
    {
        try
        {
            return _context.Viewings
                .Include(v => v.Theatre)
                .Include(v => v.ViewingSeats)
                .Include(v => v.Movie)
                .First(s => s.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
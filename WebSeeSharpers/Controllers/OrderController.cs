using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebSeeSharpers.Data;
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

        var seats = GetSeats(reservedSeats, viewing);

        Debug.WriteLine(seats.Count);

        ViewBag.Seats = seats.ToArray();
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

    private List<Seat> GetSeats(string seatsPositions, Viewing viewing)
    {
        var seatPositionsSplit = seatsPositions.Split(';');
        List<Vector2> vectorList = new();
        foreach (var position in seatPositionsSplit)
        {
            var positionXAndY = position.Split('_');
            if (positionXAndY.Length < 2)
                continue;

            vectorList.Add(new(Convert.ToSingle(positionXAndY[0]), Convert.ToSingle(positionXAndY[1])));
            Debug.WriteLine(position);
        }

        return GetSeatsFromVectorList(vectorList, viewing);
    }

    private List<Seat> GetSeatsFromVectorList(List<Vector2> positions, Viewing viewing)
    {
        SeatService seatService = new(viewing, _context);
        List<Seat?> seats = new();

        positions.ForEach(p => { seats.Add(seatService.GetSeat(p)); });
        return seats;
    }
}
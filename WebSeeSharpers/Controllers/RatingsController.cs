using Microsoft.AspNetCore.Mvc;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Controllers
{
    public class RatingsController : Controller
    {
        private readonly WebSeeSharpersContext _context;

        public RatingsController(WebSeeSharpersContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetComment(int Id, string comment, string viewername)
        {
            Rating rating = new Rating();
            rating.Comment = comment;
            rating.MovieId = Id;
            rating.ViewerName = viewername;


            _context.Add(rating);  
            _context.SaveChanges();

            return RedirectToAction("Details", "Movies", new { id = Id });


        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebSeeSharpersContext _context;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(WebSeeSharpersContext context, ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var movies = from m in _context.Movie
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Title.Contains(searchString) || m.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "Date":
                    movies = movies.OrderBy(m => m.BeginTime);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(m => m.BeginTime);
                    break;
                default:
                    movies = movies.OrderBy(s => s.Title);
                    break;
            }

            return View(await movies.ToListAsync());
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {Expires = DateTimeOffset.Now.AddDays(30)});

            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Viewings()
        {
            return RedirectToAction("Index", "Viewings");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
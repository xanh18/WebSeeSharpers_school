using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSeeSharpers.Data;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Controllers;

public class NewsletterController : Controller
{
    private readonly WebSeeSharpersContext _context;

    public NewsletterController(WebSeeSharpersContext context)
    {
        _context = context;
    }

    /**
     * Store the email when someone's wants to subscribe to the newsletter.
     */
    [HttpPost]
    public IActionResult store(string newsletterEmail)
    {
        if (!newsletterEmail.Contains("@") || !newsletterEmail.Contains("."))
        {
            return RedirectToAction("WrongEmail"); ;
        }
        
        _context.Newsletter.Add(new Newsletter() {email = newsletterEmail});
        _context.SaveChanges();

        return RedirectToAction("ThankYou");
    }

    public IActionResult ThankYou()
    {
        return View();
    }

    public IActionResult WrongEmail()
    {
        return View();
    }
}
using BakeryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Modern_Bakery.Data;
using Modern_Bakery.Models;
using System;
using System.Diagnostics;

namespace Modern_Bakery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // ✅ ADD THIS
        private readonly ApplicationDbContext context;

        // ✅ UPDATE CONSTRUCTOR
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var reviews = context.ContactMessages
                                 .OrderByDescending(x => x.Id)
                                 .Take(5)
                                 .ToList();

            ViewBag.Reviews = reviews;

            // ⭐ Average Rating
            double avg = 0;
            if (context.ContactMessages.Any())
            {
                avg = context.ContactMessages.Average(x => x.Rating);
            }

            ViewBag.Rating = Math.Round(avg, 1);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // ✅ FIXED POST METHOD
        [HttpPost]
        public IActionResult Contact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                context.ContactMessages.Add(model);
                context.SaveChanges();

                ViewBag.Success = "Message sent successfully!";
                ModelState.Clear();
                TempData["Success"] = "Feedback submitted!";
                return RedirectToAction("Index",Review); // 👈 AUTO SHOW ON HOME
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult Rating()
        {
            var ratings = context.ContactMessages.ToList();

            double avgRating = 0;

            if (ratings.Count > 0)
            {
                avgRating = ratings.Average(x => x.Rating);
            }

            ViewBag.Rating = Math.Round(avgRating, 1); // ex: 4.5

            return View();
        }

        public IActionResult Review()
        {
            var reviews = context.ContactMessages
                                 .OrderByDescending(x => x.Id)
                                 .ToList();

            return View(reviews);
        }

        public IActionResult Gallery()
        {
            return View();
        }
    }
}

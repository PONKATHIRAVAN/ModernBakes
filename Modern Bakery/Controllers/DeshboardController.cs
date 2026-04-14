using Microsoft.AspNetCore.Mvc;
using Modern_Bakery.Data;
using System;

namespace BakeryShop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext context;

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProducts = context.Products.Count();
            return View();
        }
    }
}
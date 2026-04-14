using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "Kathir" && password == "Kathir@2324")
            {
                HttpContext.Session.SetString("Admin", "true");

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
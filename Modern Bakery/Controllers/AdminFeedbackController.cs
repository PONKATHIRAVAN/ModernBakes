using BakeryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Modern_Bakery.Data;
using System;

namespace BakeryShop.Controllers
{
    public class AdminFeedbackController : Controller
    {
        private readonly ApplicationDbContext context;

        public AdminFeedbackController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // 🔐 ADMIN CHECK
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Admin") == "true";
        }

        // 📄 VIEW ALL FEEDBACK
        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var data = context.ContactMessages
                              .OrderByDescending(x => x.Id)
                              .ToList();

            return View(data);
        }

        // ✏ EDIT
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var data = context.ContactMessages.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ContactMessage model)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var data = context.ContactMessages.Find(model.Id);

            if (data == null)
                return NotFound();

            // ✅ Update only required fields
            data.Name = model.Name;
            data.Message = model.Message;
            data.Rating = model.Rating;

            // ❗ Keep existing Email (important)
            data.Email = data.Email;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ❌ DELETE
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var data = context.ContactMessages.Find(id);

            context.ContactMessages.Remove(data);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
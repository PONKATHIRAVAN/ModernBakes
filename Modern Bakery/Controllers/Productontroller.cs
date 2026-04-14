using BakeryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Modern_Bakery.Data;
using System;

namespace BakeryShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        } 
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        // PRODUCT DETAILS PAGE
        public IActionResult Details(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // ADMIN ONLY
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile Image)
        {
            if (Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);

                string folder = Path.Combine(env.WebRootPath, "images");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                product.ImagePath = fileName;
            }

            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login", "Account");

            var product = context.Products.Find(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login", "Account");

            var existingProduct = context.Products.Find(product.Id);

            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ADMIN ONLY
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login", "Account");

            var product = context.Products.Find(id);

            context.Products.Remove(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

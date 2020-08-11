using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Threading.Tasks.Dataflow;

using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
             List<Product> products = await _context.Products.ToListAsync();


            return View(products);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(Product p)


        {
            if (ModelState.IsValid)
            {
                // add to database
                _context.Products.Add(p);

                await _context.SaveChangesAsync();


                TempData["Message"] = $"{p.Title} was added successfully";

                // redirect to catalog page
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}

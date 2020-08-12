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

        public async Task<IActionResult> Index(int? id)
        {
            int pageNumber = id ?? 1;
            const int perPage = 3;

            int totalProducts = await ProductDB.GetTotalProductsAsync(_context);
            int totalPages = (int)Math.Ceiling((double)totalProducts / perPage);

            pageNumber = Math.Min(pageNumber, totalPages);

            ViewData["totalPages"] = totalPages;
            ViewData["pageNumber"] = pageNumber;
            List<Product> products = await ProductDB.GetProductsAsync(_context, pageNumber, perPage);

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
                await ProductDB.AddProductAsync(_context, p);

                TempData["Message"] = $"{p.Title} was added successfully";

                // redirect to catalog page
                return RedirectToAction("Index");
            }

            return View();
        }

        

        public async Task<IActionResult> Edit(int id)
        {
            Product p = await ProductDB.GetProductByIdAsync(_context, id);

            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                await ProductDB.EditProductAsync(_context ,p);

                ViewData["message"] = "Product updated successfully!";
            }
            return View(p);
        }

        

        public async Task<IActionResult> Delete(int id)
        {
            Product p = await ProductDB.GetProductByIdAsync(_context, id);

            return View(p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmAsync(int id)
        {
            await ProductDB.DeleteProductAsync(_context, id);

            TempData["Message"] = "Product removed successfully";

            // redirect to catalog page
            return RedirectToAction("Index");
        }

        

    }
}

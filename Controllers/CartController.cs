using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// dds a product to the shopping cart
        /// </summary>
        /// <param name="id">the id of the product to add</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id, string prevUrl)
        {

            // grab item from db
            Product p = await ProductDB.GetProductByIdAsync(_context, id);

            CookieHelper.AddItemToCart(_httpContext, p);

            TempData["Message"] = $"{p.Title} added successfully!";

            // redirect them back to index
            return Redirect(prevUrl);
        }

        public IActionResult Summary()
        {

            List<Product> cartProducts = CookieHelper.GetProductsFromCart(_httpContext);
            return View(cartProducts);
        }
    }
}

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
        public async Task<IActionResult> Add(int id)
        {
            // grab item from db
            Product p = await ProductDB.GetProductByIdAsync(_context, id);
            // add item to cookies
            string data = JsonConvert.SerializeObject(p);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                Secure = true,
                IsEssential = true,
            };

            _httpContext.HttpContext.Response.Cookies.Append("cartCookie", data, options);

            // redirect them back to index
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            //display all items in shopping cart

            return View();
        }
    }
}

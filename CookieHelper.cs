using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce
{
    /// <summary>
    /// A helper class for sending and retrieving
    /// </summary>
    public static class CookieHelper
    {
        /// <summary>
        /// The Key used in getting the cart cookies
        /// </summary>
        public const string cartCookie = "cartCookie";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessor">The IHttpContextAccessor that has the cookies</param>
        /// <returns>A list of products, filled with the items in the cart.</returns>
        public static List<Product> GetProductsFromCart(IHttpContextAccessor accessor)
        {
            string existingItems = accessor.HttpContext.Request.Cookies[cartCookie];
            // add new item to old items
            List<Product> cartList = new List<Product>();
            if (existingItems != null)
            {
                cartList = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }

            return cartList;
        }

        /// <summary>
        /// adds a product to the cookies
        /// </summary>
        /// <param name="accessor">The IHttpContextAccessor that has the cookies</param>
        /// <param name="p">The product to add to the cookies</param>
        public static void AddItemToCart(IHttpContextAccessor accessor, Product p)
        {
            List<Product> cartList = GetProductsFromCart(accessor);
            cartList.Add(p);
            
            string data = JsonConvert.SerializeObject(cartList);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                Secure = true,
                IsEssential = true,
            };

            accessor.HttpContext.Response.Cookies.Append(cartCookie, data, options);
        }

        /// <summary>
        /// returns the total number of products in the cart
        /// </summary>
        /// <param name="accessor">The IHttpContextAccessor that has the cookies</param>
        public static int GetTotalNumberOfCartProducts(IHttpContextAccessor accessor)
        {
            return GetProductsFromCart(accessor).Count;
        }
    }
}

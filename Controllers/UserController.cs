using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;
        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel RVM)
        {
            if (ModelState.IsValid)
            {
                // map data
                UserAccount userAccount = new UserAccount() {
                    Username = RVM.Username,
                    Email = RVM.Email,
                    Password = RVM.Password };
                // add to databasa
                await _context.userAccounts.AddAsync(userAccount);
                // redirect home
                return RedirectToAction("Index", "Home");
            }
            return View(RVM);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

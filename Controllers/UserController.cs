using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            UserAccount account = await _context.userAccounts
                .Where(u => u.Username == model.Username && u.Password == model.Password)
                .SingleOrDefaultAsync();

            if(account == null)
            {
                ModelState.AddModelError(string.Empty, "Credentials not found");

                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", account.ID);

            return RedirectToAction("Index", "Home");
        }
    }
}

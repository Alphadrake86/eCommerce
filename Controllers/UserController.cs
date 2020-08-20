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
                bool isEmailTaken = await _context.userAccounts
                    .Where(u => u.Email == RVM.Email)
                    .AnyAsync();
                bool isUsernameTaken = await _context.userAccounts
                    .Where(u => u.Username == RVM.Username)
                    .AnyAsync();
                if (isEmailTaken || isUsernameTaken)
                {
                    if (isUsernameTaken)
                        ModelState.AddModelError(nameof(RegisterViewModel.Username), "That username is already taken");
                    if (isEmailTaken)
                        ModelState.AddModelError(nameof(RegisterViewModel.Email), "That email is already in use");
                    return View(RVM);
                }
                // map data
                UserAccount userAccount = new UserAccount() {
                    Username = RVM.Username,
                    Email = RVM.Email,
                    Password = RVM.Password };
                // add to databasa
                await _context.userAccounts.AddAsync(userAccount);
                await _context.SaveChangesAsync();

                LogUserIntoSession(userAccount);
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
                .Where(u => (u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail) && u.Password == model.Password)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                ModelState.AddModelError(string.Empty, "Credentials not found");

                return View(model);
            }

            LogUserIntoSession(account);

            return RedirectToAction("Index", "Home");
        }

        private void LogUserIntoSession(UserAccount account)
        {
            HttpContext.Session.SetInt32("UserId", account.ID);
            HttpContext.Session.SetString("Username", account.Username);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}

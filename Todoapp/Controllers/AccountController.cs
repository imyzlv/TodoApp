using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todoapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Todoapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly TodoDbContext _db;

        public AccountController(TodoDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.userAccount.ToList());
        }

        public IActionResult Register()
        {
            return View();
        }

        //Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                _db.userAccount.Add(account);
                _db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + " was successfully registered.";
            }
            return View();
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserAccount user)
        {
            var usr = _db.userAccount.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (usr != null)
            {
                HttpContext.Session.SetString("UserId", usr.UserId.ToString());
                HttpContext.Session.SetString("Username", usr.Username.ToString());
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View();
        }

        public IActionResult LoggedIn()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
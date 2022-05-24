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
        //DbContextOptions options = new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;

        public IActionResult Index()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            using (TodoDbContext db = new TodoDbContext(options))
            {
                return View(db.userAccount.ToList());
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        //Register
        [HttpPost]
        public IActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                var options = new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
                using (TodoDbContext db = new TodoDbContext(options))
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
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
        public IActionResult Login(UserAccount user)
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            using (TodoDbContext db = new TodoDbContext(options))
            {
                var usr = db.userAccount.Single(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    HttpContext.Session.SetString("UserId", usr.UserId.ToString());
                    HttpContext.Session.SetString("UserName", usr.Username.ToString());
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Error - username or password is not correct");
                }
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
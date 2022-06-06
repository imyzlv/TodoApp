using Microsoft.AspNetCore.Mvc;
using Todoapp.Models;
using Todoapp.Database;

namespace Todoapp.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly TodoDbContext _db;

        public ToDoListController(TodoDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ToDoLists.ToList());
        }
    }
}

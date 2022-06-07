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
        //Add Task
        public IActionResult AddTask(ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _db.ToDoLists.Add(toDoList);
                _db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Task was succesfully added";
            }
                return View();
        }
    }
}

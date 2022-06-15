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
        //Edit Task
        //[HttpGet]
        [HttpGet]
        public IActionResult EditTask(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var task = _db.ToDoLists.Find(Id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult EditTask(int Id, ToDoList toDoList)
        {
            var taskFromDb = _db.ToDoLists.Find(Id);
            if(taskFromDb == null)
            {
                return NotFound();
            }
            taskFromDb.TaskText = toDoList.TaskText.ToString();
            taskFromDb.TaskList = toDoList.TaskList.ToString();

            _db.ToDoLists.Update(taskFromDb);
            _db.SaveChanges();
            ModelState.Clear();
            ViewBag.Message = "Task was succesfully updated";

            return View();
            //return RedirectToAction("Index");
        }

    }
}

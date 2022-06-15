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
        //[HttpPost]
        public async Task<IActionResult> EditTask(int id, ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return NotFound();
            }
            //await _db.ToDoLists.FindAsync(id);
            if (ModelState.IsValid)
            {
                _db.ToDoLists.Update(toDoList);
                await _db.SaveChangesAsync();
                //ModelState.Clear();
                ViewBag.Message = "Task was succesfully updated";
            }

            return View(toDoList);
        }
        public async Task<IActionResult> SaveTask(int id, ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return NotFound();
            }
            //await _db.ToDoLists.FindAsync(id);
            if (ModelState.IsValid)
            {
                _db.ToDoLists.Update(toDoList);
                await _db.SaveChangesAsync();
                //ModelState.Clear();
                ViewBag.Message = "Task was succesfully updated";
            }

            return View(toDoList);
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Todoapp.Models;
using Todoapp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Todoapp.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly TodoDbContext _db;

        public ToDoListController(TodoDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string taskListCheck)
        {
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {

                return Redirect("/Identity/Account/Login");
            }
            else
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                IQueryable<string> listQuery = from m in _db.ToDoLists
                                               orderby m.TaskList
                                               select m.TaskList;
                var task = from m in _db.ToDoLists
                           where m.UserId == userId
                           select m;

                if (!string.IsNullOrEmpty(taskListCheck))
                {
                    task = task.Where(x => x.TaskList == taskListCheck);
                }

                var taskListViewList = new TaskListModel
                {
                    TaskList = new SelectList(await listQuery.Distinct().ToListAsync()),
                    ToDoLists = await task.ToListAsync()
                };

                return View(taskListViewList);
            }
        }
        //Add Task
        [HttpGet]
        public IActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTask(ToDoList toDoList)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            ModelState.Remove("UserAccount");
            try
            {
                toDoList.UserId = userId;
                _db.ToDoLists.Add(toDoList);
                _db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Task was succesfully added";
            }
            catch (DbUpdateException)
            {
                    ViewBag.Message = "Do not leave empty fields!";      
            }
            return View();

        }
        //Edit Task
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
            if (taskFromDb == null)
            {
                return NotFound();
            }
            taskFromDb.Title = toDoList.Title.ToString();
            taskFromDb.TaskText = toDoList.TaskText.ToString();
            taskFromDb.TaskList = toDoList.TaskList.ToString();
            taskFromDb.TaskLevel = toDoList.TaskLevel;
            taskFromDb.DateTimeFinal = toDoList.DateTimeFinal;
            taskFromDb.TaskDone = toDoList.TaskDone;

            _db.ToDoLists.Update(taskFromDb);
            _db.SaveChanges();
            ViewBag.Message = "Task was succesfully updated";

            return View();
        }

        //Details about task
        public async Task<IActionResult> DetailsTask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _db.ToDoLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        //Delete task

        public async Task<IActionResult> DeleteTask(int? id)
        {
            if (id == null || _db.ToDoLists == null)
            {
                return NotFound();
            }

            var task = await _db.ToDoLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("DeleteTask")]
        public async Task<IActionResult> DeleteTaskConfirmed(int id)
        {
            if (_db.ToDoLists == null)
            {
                return Problem("Entity set 'ToDoList'  is null.");
            }
            var task = await _db.ToDoLists.FindAsync(id);
            if (task != null)
            {
                _db.ToDoLists.Remove(task);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Task done/undone
        public IActionResult TaskDone(int id, ToDoList toDoList)
        {
            var taskFromDb = _db.ToDoLists.Find(id);
            if (taskFromDb == null)
            {
                return NotFound();
            }
            taskFromDb.TaskDone = !taskFromDb.TaskDone;

            _db.ToDoLists.Update(taskFromDb);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}

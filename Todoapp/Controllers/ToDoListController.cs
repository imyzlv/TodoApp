using Microsoft.AspNetCore.Mvc;
using Todoapp.Models;
using Todoapp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Todoapp.Controllers
{
    //Use authorization
    [Authorize]
    public class ToDoListController : Controller
    {
        private readonly TodoDbContext _db;

        //Global variable for current user Id.
        public static string userId = null;

        public ToDoListController(TodoDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string taskListCheck)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            // Fetch "lists" for filtering
            IQueryable<string> listQuery = from m in _db.ToDoLists
                                           where m.UserId == userId && !m.TaskDone
                                           orderby m.TaskList
                                           select m.TaskList;
            //Fetch data from 2 tables: tasks from the 1st one and creator from the other
            var task = from m in _db.ToDoLists
                       where ((m.UserId == userId) || (m.PublicTask == true)) && (!m.TaskDone)
                       select m;

            var userNameFromDb = from c in _db.Users
                                 select c;
            if (!string.IsNullOrEmpty(taskListCheck))
            {
                task = task.Where(x => x.TaskList == taskListCheck);
            }

            var taskListViewList = new TaskListModel
            {
                TaskList = new SelectList(await listQuery.Distinct().ToListAsync()),
                ToDoLists = await task.ToListAsync(),
                UserList = userNameFromDb.ToList()
            };

            return View(taskListViewList);

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
            ModelState.Remove("UserAccount");
            try
            {
                toDoList.UserId = userId;
                _db.ToDoLists.Add(toDoList);
                _db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Task was succesfully added";
                TempData["success"] = "Task added successfully.";
            }
            catch (DbUpdateException)
            {
                ViewBag.Message = "Do not leave empty fields!";
            }
            //return RedirectToAction("Index");
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

            if (task == null || task.UserId != userId)
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
            if (taskFromDb.UserId == userId)
            {
                try
                {
                    taskFromDb.Title = toDoList.Title.ToString();
                    taskFromDb.TaskText = toDoList.TaskText.ToString();
                    taskFromDb.TaskList = toDoList.TaskList.ToString();
                    taskFromDb.TaskLevel = toDoList.TaskLevel;
                    taskFromDb.DateTimeFinal = toDoList.DateTimeFinal;
                    taskFromDb.TaskDone = toDoList.TaskDone;

                    _db.ToDoLists.Update(taskFromDb);
                    _db.SaveChanges();
                    TempData["success"] = "Task updated successfully.";
                    ViewBag.Message = "Task was succesfully updated";
                }
                catch (NullReferenceException)
                {
                    ViewBag.Message = "Do not leave empty fields!";
                }

                return View(taskFromDb);
            }
            else
            {
                return NotFound();
            }
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
            TempData["success"] = "Task deleted.";
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
            //Use this to also save "finished on" time
            taskFromDb.DateTime = DateTime.Now;
            _db.ToDoLists.Update(taskFromDb);
            _db.SaveChanges();
            TempData["success"] = taskFromDb.Title.ToString() + " is completed!";
            return RedirectToAction(nameof(Index));
        }

        //Method to load finished tasks into a partial view
        //TODO: fetch also tasks not belonging to the current user
        //for that - add another field to ToDoList and then fetch data
        //pointing to the user that closed the task
        public IActionResult LoadCompletedTasks()
        {
            var completedItems = from b in _db.ToDoLists
                                 where (b.UserId == userId) && (b.TaskDone == true)
                                 select b;
            return PartialView("CompletedTasks", completedItems.ToList());
        }

    }
}

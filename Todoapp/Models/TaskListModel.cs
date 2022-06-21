using Microsoft.AspNetCore.Mvc.Rendering;
using Todoapp.Database;

namespace Todoapp.Models
{
    public class TaskListModel
    {
        public List<ToDoList>? ToDoLists { get; set; }

        public SelectList? TaskList { get; set; }
        public string? TaskListCheck { get; set; }
    }
}

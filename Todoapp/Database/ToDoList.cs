using System;
using System.ComponentModel.DataAnnotations;
namespace Todoapp.Database
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string TaskText { get; set; }

        [Required]
        public string TaskList { get; set; }
        public int TaskLevel { get; set; }

        public static DateTime Now { get; }
    }
}

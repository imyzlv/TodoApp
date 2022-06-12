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

        [Display(Name = "Task text")]
        public string TaskText { get; set; }

        [Required]
        [Display(Name = "Task list")]
        public string TaskList { get; set; }
        public enum TaskLevelEnum 
        {
            [Display(Name = "Very low")]
            VeryLow,
            Low,
            Normal,
            High,
            [Display(Name = "Very high")]
            VeryHigh
        }
        [Display(Name = "Task level")]
        public TaskLevelEnum TaskLevel { get; set; }
        public DateTime DateTime { get; } = DateTime.Now;
        [Display(Name = "Done")]
        public bool TaskDone { get; set; }
    }
}

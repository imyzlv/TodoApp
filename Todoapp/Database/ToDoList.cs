using System.ComponentModel.DataAnnotations;
using Todoapp.Models;

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
        
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        [DataType(DataType.Date)]
        [Display(Name = "Due time")]
        public DateTime DateTimeFinal { get; set; }

        [Display(Name = "Done")]
        public bool TaskDone { get; set; }
        
        public UserAccount UserAccount { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todoapp.Database;
using Todoapp.Models;

namespace Todoapp.Database
{
    public static class SeedDataTaskList
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TodoDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TodoDbContext>>()))
            {
                // Look for any Tasks.
                if (context.ToDoLists.Any())
                {
                    return;   // DB has been seeded
                }

                context.ToDoLists.AddRange(
                    new ToDoList
                    {
                        Title = "Tīrīt",
                        TaskText = "Satīrīt galdu",
                        TaskList = "Mājas",
                        TaskLevel = ToDoList.TaskLevelEnum.Low,
                        DateTimeFinal = DateTime.Parse("2022-6-30"),
                        TaskDone = false
                    },

                    new ToDoList
                    {
                        Title = "Iegādāties",
                        TaskText = "Nomainīt izdegušo spuldzīti vanasistabā",
                        TaskList = "Mājas",
                        TaskLevel = ToDoList.TaskLevelEnum.High,
                        DateTimeFinal = DateTime.Parse("2022-6-29"),
                        TaskDone = false
                    },

                    new ToDoList
                    {
                        Title = "Tests",
                        TaskText = "Sagatavot visus nepieciešamos dokumentus testiem",
                        TaskList = "Darbs",
                        TaskLevel = ToDoList.TaskLevelEnum.VeryLow,
                        DateTimeFinal = DateTime.Parse("2022-12-1"),
                        TaskDone = true
                    },

                    new ToDoList
                    {
                        Title = "Rēķini",
                        TaskText = "Veikt gāzaes rēķina pārskaitījumu",
                        TaskList = "Mājas",
                        TaskLevel = ToDoList.TaskLevelEnum.VeryHigh,
                        DateTimeFinal = DateTime.Parse("2022-5-19"),
                        TaskDone = false
                    },

                    new ToDoList
                    {
                        Title = "Brīvdiena",
                        TaskText = "Pieteikt brīvu dienu",
                        TaskList = "Darbs",
                        TaskLevel = ToDoList.TaskLevelEnum.High,
                        DateTimeFinal = DateTime.Parse("2022-7-1"),
                        TaskDone = false
                    },

                    new ToDoList
                    {
                        Title = "Izbrauciens",
                        TaskText = "Izveidot atpūtas brauciena maršrutu",
                        TaskList = "Brīvdienas",
                        TaskLevel = ToDoList.TaskLevelEnum.Normal,
                        DateTimeFinal = DateTime.Parse("2022-7-7"),
                        TaskDone = true
                    }

                );
                context.SaveChanges();
            }
        }
    }
}

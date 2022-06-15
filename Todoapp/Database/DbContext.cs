using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Todoapp.Database;

namespace Todoapp.Models
{
    public class TodoDbContext : IdentityDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<UserAccount> userAccount { get; set; }

        public DbSet<ToDoList> ToDoLists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .HasData(
                    new ToDoList
                    {
                        Id = 1,
                        Title = "Test title",
                        TaskText = "Full task text",
                        TaskList = "List type",
                        //TaskLevel = 1,
                    },
                    new ToDoList
                    {
                        Id = 2,
                        Title = "Test title 2",
                        TaskText = "Full task text 2",
                        TaskList = "List type 2",
                        //TaskLevel = 2,
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}


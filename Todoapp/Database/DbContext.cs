using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Todoapp.Database;
using Microsoft.AspNetCore.Identity;

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");

            // Rename default scaffolded table names
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            modelBuilder.Entity<ToDoList>()
                .HasData(
                    new ToDoList
                    {
                        Id = 1,
                        Title = "Test title",
                        TaskText = "Full task text",
                        TaskList = "List type",
                        TaskLevel = ToDoList.TaskLevelEnum.Low,
                    },
                    new ToDoList
                    {
                        Id = 2,
                        Title = "Test title 2",
                        TaskText = "Full task text 2",
                        TaskList = "List type 2",
                        TaskLevel = ToDoList.TaskLevelEnum.Low,
                    }
                );

        }
    }
}


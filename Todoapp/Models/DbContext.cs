using System;
using Microsoft.EntityFrameworkCore;

namespace Todoapp.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        public DbSet<UserAccount> userAccount { get; set; }
    }
}


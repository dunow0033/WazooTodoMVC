using Microsoft.EntityFrameworkCore;
using WazooTodoMVC.Models;

namespace WazooTodoMVC.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>()
                .HasData(new List<TodoItem>()
                {
                    new()
                    {
                        Id = 1,
                        Description = "Buy Groceries"
                    },
                    new()
                    {
                        Id = 2,
                        Description = "Wash Car"
                    }
                });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WazooTodoMVC.Models;

namespace WazooTodoMVC.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}

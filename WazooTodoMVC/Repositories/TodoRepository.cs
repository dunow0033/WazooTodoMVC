using Microsoft.EntityFrameworkCore;
using WazooTodoMVC.Data;
using WazooTodoMVC.Models;
using WazooTodoMVC.Models.ViewModels;

namespace WazooTodoMVC.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext todoDbContext;

        public TodoRepository(TodoDbContext todoDbContext)
        {
            this.todoDbContext = todoDbContext;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await todoDbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> AddAsync(TodoItem todoItem)
        {
            await todoDbContext.AddAsync(todoItem);
            await todoDbContext.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem?> GetAsync(long id)
        {
            return await todoDbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TodoItem?> UpdateAsync(TodoItem todoItem)
        {
            var existingTodo = await todoDbContext.TodoItems.FindAsync(todoItem.Id);

            if (existingTodo != null)
            {
                existingTodo.Description = todoItem.Description;

                await todoDbContext.SaveChangesAsync();
                return existingTodo;
            }

            return null;
        }

        public async Task<TodoItem?> DeleteAsync(long id)
        {
            var existingTodo = await todoDbContext.TodoItems.FindAsync(id);

            if (existingTodo != null)
            {
                todoDbContext.Remove(existingTodo);
                await todoDbContext.SaveChangesAsync();

                return existingTodo;
            }
            return null;
        }
    }
}

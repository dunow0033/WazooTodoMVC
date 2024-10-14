using WazooTodoMVC.Models;

namespace WazooTodoMVC.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetAsync(long id);
        Task<TodoItem?> AddAsync(TodoItem todoitem);
        Task<TodoItem?> UpdateAsync(TodoItem todoItem);
        Task<TodoItem?> DeleteAsync(long id);
    }
}

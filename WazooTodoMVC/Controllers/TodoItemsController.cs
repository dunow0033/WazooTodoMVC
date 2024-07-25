using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WazooTodoMVC.Data;

namespace WazooTodoMVC.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly TodoDbContext _context;
        public TodoItemsController(TodoDbContext context)
        {
                _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var todoItems = await _context.TodoItems.ToListAsync();

            return View(todoItems);
        }
    }
}

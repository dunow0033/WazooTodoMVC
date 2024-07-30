using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WazooTodoMVC.Data;
using WazooTodoMVC.Models;
using WazooTodoMVC.Models.ViewModels;

namespace WazooTodoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodoDbContext _context;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(TodoDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var todoItems = await _context.TodoItems.ToListAsync();

            return View(todoItems);
        }

        [HttpGet]
        public IActionResult CreateTodo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTodo(SubmitTodoRequest submitTodoRequest)
        {
            var todo = new TodoItem
            {
                Description = submitTodoRequest.Description
            };

            _context.TodoItems.Add(todo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTodo(long id)
        {
            var editTodo = _context.TodoItems.FirstOrDefault(x => x.Id == id);

            if (editTodo != null)
            {
                var editTodoRequest = new EditTodoRequest
                {
                    Id = editTodo.Id,
                    Description = editTodo.Description
                };

                return View(editTodoRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult EditTodo(EditTodoRequest editTodoRequest)
        {
            var todo = new TodoItem
            {
                Id = editTodoRequest.Id,
                Description = editTodoRequest.Description
            };

            var existingTodo = _context.TodoItems.Find(todo.Id);

            if(existingTodo != null)
            {
                existingTodo.Description = editTodoRequest.Description;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("EditTodo", new { id = editTodoRequest.Id });
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTodo([FromBody] long id)
        {

            var deleteTodo = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == (id + 1));

            if (deleteTodo != null)
            {
                _context.TodoItems.Remove(deleteTodo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

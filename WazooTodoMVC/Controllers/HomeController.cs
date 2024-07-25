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
        public IActionResult Edit()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

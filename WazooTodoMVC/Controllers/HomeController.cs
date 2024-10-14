using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WazooTodoMVC.Data;
using WazooTodoMVC.Models;
using WazooTodoMVC.Models.ViewModels;
using WazooTodoMVC.Repositories;

namespace WazooTodoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ITodoRepository todoRepository;

        public HomeController(ITodoRepository todoRepository, ILogger<HomeController> logger)
        {
            this.todoRepository = todoRepository;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var todoItems = await todoRepository.GetAllAsync();

            return View(todoItems);
        }

        [HttpGet]
        public IActionResult CreateTodo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(SubmitTodoRequest submitTodoRequest)
        {
            var todo = new TodoItem
            {
                Description = submitTodoRequest.Description
            };

            await todoRepository.AddAsync(todo);

            TempData["Description"] = todo.Description;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditTodo(long id)
        {
            var editTodo = await todoRepository.GetAsync(id);

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
        public async Task<IActionResult> EditTodo(EditTodoRequest editTodoRequest)
        {
            var todo = new TodoItem
            {
                Id = editTodoRequest.Id,
                Description = editTodoRequest.Description
            };

            var existingTodo = await todoRepository.UpdateAsync(todo);

            TempData["EditedTodoId"] = existingTodo.Id.ToString();

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTodo(long id, string name)
        {
            var deleteTodo = await todoRepository.DeleteAsync(id);

            if (deleteTodo != null)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = $"Todo item '{name}' was not found." });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

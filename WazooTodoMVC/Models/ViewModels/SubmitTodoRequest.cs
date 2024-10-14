using System.ComponentModel.DataAnnotations;

namespace WazooTodoMVC.Models.ViewModels
{
    public class SubmitTodoRequest
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}

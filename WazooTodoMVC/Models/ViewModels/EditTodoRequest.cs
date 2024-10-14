using System.ComponentModel.DataAnnotations;

namespace WazooTodoMVC.Models.ViewModels
{
    public class EditTodoRequest
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description {  get; set; }
    }
}

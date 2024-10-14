using System.ComponentModel.DataAnnotations;

namespace WazooTodoMVC.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MiniJiraWeb.Models
{
    public class LoginUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Password { get; set; }
    }
}

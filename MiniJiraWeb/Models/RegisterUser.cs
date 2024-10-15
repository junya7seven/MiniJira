using System.ComponentModel.DataAnnotations;

namespace MiniJiraWeb.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(6,ErrorMessage = "Минимальная длина 6 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Неверный формат Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(6, ErrorMessage = "Минимальная длина 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Compare("Password",ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}

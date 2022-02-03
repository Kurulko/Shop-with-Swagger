using System.ComponentModel.DataAnnotations;

namespace Shop_with_Swagger.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите свою почту")]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}

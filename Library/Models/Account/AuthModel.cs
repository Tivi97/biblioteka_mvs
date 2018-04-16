
 using System.ComponentModel.DataAnnotations;

namespace Library.Models.Account
{
    public class AuthModel
    {
            [Required(ErrorMessage = "Введите логин")]
            public string Username { get; set; }
            [Required(ErrorMessage = "Введите пароль")]
            public string Password { get; set; }
        }
    }

using System.ComponentModel.DataAnnotations;

namespace BIV_Challange.RequestModels
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть от 5 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;


namespace Library.Models.Account
{
    public class RegistreModel
    {
        [Required(ErrorMessage = "Введите фамилию, имя и отчество")]
        [RegularExpression(@"([А-Яа-я]+\s){2}[А-Яа-я]+", ErrorMessage = "Некорректный формат ФИО")]
        public string Fio { get; set; }            

        [Required(ErrorMessage = "Введите дату рождения")]
        [RegularExpression(@"((0[1-9]|[12]\d)(\.)(0[1-9]|1[012])|30(\.)(0[13-9]|1[012])|31(\.)(0[13578]|1[02]))(\.)[0-9]{4}", ErrorMessage = "Некорректный формат даты")]
        public string Birthday { get; set; }

        [Required(ErrorMessage = "Введите серию паспорта")]
        //[RegularExpression(@"(89)[0-9]{9}", ErrorMessage = "Некорректный формат серии паспорта")]
        public string PassportSeries { get; set; }

        [Required(ErrorMessage = "Введите номер паспорта")]
        [RegularExpression(@"(89)[0-9]{9}", ErrorMessage = "Некорректный формат номера паспорта")]
        public string PassportKod { get; set; }

        [Required(ErrorMessage = "Введите номер свидетельства о рождении")]
       // [RegularExpression(@"(89)[0-9]{9}", ErrorMessage = "Некорректный формат номера паспорта")]
        public string CertificateCode { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}

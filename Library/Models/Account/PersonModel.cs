
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Account
{
    public class PersonModel
    {
        public class ProfileModel
        {
            public TouristModel Tourist { get; set; }
            public ExperienceModel Exp { get; set; }
        }

        public class TouristModel
        {
            public int Id { get; set; }
            [RegularExpression(@"([А-Яа-я]+\s){2}[А-Яа-я]+", ErrorMessage = "Некорректный формат ФИО")]
            public string Fio { get; set; }
            [RegularExpression(@"(8\-9)[0-9]{2}\-[0-9]{3}(\-[0-9]{2}){2}", ErrorMessage = "Некорректный формат номера телефона")]
            public string Phone { get; set; }
            public string Sex { get; set; }
            public string Birthday { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsCoach { get; set; }
            public bool IsBlocked { get; set; }
        }

        public class ExperienceModel
        {
            public int IdUser { get; set; }
            public int IdHike { get; set; }
        }

        public class HikeModel
        {
            public string IsLead { get; set; }
            public string Name { get; set; }
            public string Start { get; set; }
            public string Finish { get; set; }
            public string Category { get; set; }
            public string Type { get; set; }
        }
    }
}

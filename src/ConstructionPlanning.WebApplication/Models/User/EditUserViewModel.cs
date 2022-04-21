using ConstructionPlanning.WebApplication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [TranslatedRequared]
        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [TranslatedRequared]
        [Display(Name = "Фамилия")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Заполните поле \"Фамилия\"")]
        public string Surname { get; set; }

        [TranslatedRequared]
        [Display(Name = "Имя")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Заполните поле \"Имя\"")]
        public string Forename { get; set; }

        [Display(Name = "Предоставить права администратора?")]
        public bool IsAdmin { get; set; }

        public bool IsCurrentAdmin { get; set; }
    }
}

using ConstructionPlanning.WebApplication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Provider
{
    public class ProviderCreateViewModel
    {
        [TranslatedRequared]
        [Display(Name = "Наименование")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Name { get; set; }

        [TranslatedRequared]
        [Display(Name = "Адрес")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Address { get; set; }

        [TranslatedRequared]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$", ErrorMessage = "Неверный телефон поставщика.")]
        public string Phone { get; set; }
    }
}

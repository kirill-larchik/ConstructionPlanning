using ConstructionPlanning.WebApplication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Customer
{
    public class CustomerCreateViewModel
    {
        [TranslatedRequared]
        [Display(Name = "Наименование")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Name { get; set; }

        [TranslatedRequared]
        [Display(Name = "Описание")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Description { get; set; }

        [TranslatedRequared]
        [Display(Name = "Телефон")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Phone { get; set; }
    }
}

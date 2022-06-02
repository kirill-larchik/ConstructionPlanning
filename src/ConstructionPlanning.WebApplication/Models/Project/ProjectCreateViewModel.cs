using ConstructionPlanning.WebApplication.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Project
{
    public class ProjectCreateViewModel
    {
        [TranslatedRequared]
        [Display(Name = "Наименование")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Name { get; set; }

        [TranslatedRequared]
        [Display(Name = "Крайний срок")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [TranslatedRequared]
        [Display(Name = "Количество выделенных средств")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество выделенных средств должно быть больше нуля.")]
        public int AllocatedAmount { get; set; }

        [TranslatedRequared]
        [Display(Name = "Заказчик")]
        public int CustomerId { get; set; }
    }
}

using ConstructionPlanning.WebApplication.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ConstructionObject
{
    public class ConstructionObjectCreateViewModel
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
        public string ReturnUrl { get; set; }

        [TranslatedRequared]
        public int ProjectId { get; set; }

        [Display(Name = "Проект")]
        [ReadOnly(true)]
        public string ProjectName { get; set; }
    }
}

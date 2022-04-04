using ConstructionPlanning.WebApplication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Resource
{
    public class ResourceCreateViewModel
    {
        [TranslatedRequared]
        [Display(Name = "Наименование")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Name { get; set; }

        [TranslatedRequared]
        [Display(Name = "Тип ресурса")]
        public int TypeId { get; set; }

        [TranslatedRequared]
        [Display(Name = "Количество единиц на складе")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество единиц на складе должно быть больше нуля.")]
        public int AvaliableAmount { get; set; }

        [TranslatedRequared]
        [Display(Name = "Цена за единицу")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена за единицу должна быть больше нуля.")]
        public int UnitCost { get; set; }
    }
}

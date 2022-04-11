using ConstructionPlanning.WebApplication.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Sale
{
    public class SaleCreateViewModel
    {
        [TranslatedRequared]
        [Display(Name = "Дата продажи")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [TranslatedRequared]
        [Display(Name = "Ресурс")]
        public int ResourceId { get; set; }

        [TranslatedRequared]
        [Display(Name = "Заказчик")]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Максимальная длина не должна превышать 255 символов.")]
        public string Customer { get; set; }

        [TranslatedRequared]
        [Display(Name = "Количество ресурсов")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество ресурсов должно быть больше нуля.")]
        public int Count { get; set; }
    }
}

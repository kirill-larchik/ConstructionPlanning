using ConstructionPlanning.WebApplication.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Delivery
{
    public class DeliveryCreateViewModel
    {
        [TranslatedRequared]
        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }

        [TranslatedRequared]
        [Display(Name = "Ресурс")]
        public int ResourceId { get; set; }

        [TranslatedRequared]
        [Display(Name = "Дата поставки")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [TranslatedRequared]
        [Display(Name = "Цена за единицу ресурса")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена за единицу должна быть больше нуля.")]
        public int UnitCost { get; set; }

        [Display(Name = "Количество ресурсов")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество ресурсов должно быть больше нуля.")]
        public int Count { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Delivery
{
    public class DeliveryViewModel
    {
        [Display(Name = "Номер поставки")]
        public int Id { get; set; }

        [Display(Name = "Поставщик")]
        public string ProviderName { get; set; }

        [Display(Name = "Ресурс")]
        public string ResourceName { get; set; }

        [Display(Name = "Дата поставки")]
        public DateTime Date { get; set; }

        [Display(Name = "Цена за единицу ресурса")]
        public int UnitCost { get; set; }

        [Display(Name = "Количество ресурсов")]
        public int Count { get; set; }

        [Display(Name = "Общая стоимость")]
        public int Cost { get; set; }
    }
}

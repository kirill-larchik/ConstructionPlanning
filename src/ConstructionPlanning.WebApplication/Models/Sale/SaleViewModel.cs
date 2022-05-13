using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Sale
{
    public class SaleViewModel
    {
        [Display(Name = "Номер продажи")]
        public int Id { get; set; }

        [Display(Name = "Дата продажи")]
        public DateTime Date { get; set; }

        [Display(Name = "Ресурс")]
        public string ResourceName { get; set; }

        [Display(Name = "Заказчик")]
        public string Customer { get; set; }

        [Display(Name = "Количество ресурсов")]
        public int Count { get; set; }

        [Display(Name = "Цена за единицу ресурса")]
        public int UnitCost { get; set; }

        [Display(Name = "Общая стоимость")]
        public int TotalCost { get; set; }
    }
}

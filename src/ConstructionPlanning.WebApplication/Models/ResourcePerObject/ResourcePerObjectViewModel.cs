using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ResourcePerObject
{
    public class ResourcePerObjectViewModel
    {
        [Display(Name = "Номер ресурса")]
        public int Id { get; set; }

        [Display(Name = "Строительный объект")]
        public string ConstructionObjectName { get; set; }

        [Display(Name = "Ресурс")]
        public string ResourceName { get; set; }

        [Display(Name = "Выделенное количество ресурсов")]
        public int Count { get; set; }

        [Display(Name = "Итоговые затраты")]
        public int TotalCost { get; set; }

        [Display(Name = "Количество ресурсов на слкаде")]
        public int AvaliableResourceCount { get; set; }

        public string ReturnUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Resource
{
    public class ResourceViewModel
    {
        [Display(Name = "Номер ресурса")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Тип ресурса")]
        public string TypeName { get; set; }

        [Display(Name = "Количество единиц на складе")]
        public int AvaliableAmount { get; set; }

        [Display(Name = "Цена за единицу ресурса")]
        public int UnitCost { get; set; }
    }
}

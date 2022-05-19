using ConstructionPlanning.WebApplication.Models.ResourcePerObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ConstructionObject
{
    public class ConstructionObjectViewModel
    {
        [Display(Name = "Номер объекта")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Проект")]
        public string ProjectName { get; set; }

        [Display(Name = "Итоговые затраты")]
        public int TotalCost { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<ResourcePerObjectViewModel> ResourcesPerObject { get; set; }

        public ResourcePerObjectViewModel ResourcePerObjectViewModel { get; set; }
    }
}

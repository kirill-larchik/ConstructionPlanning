using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ResourceType
{
    public class ResourceTypeViewModel
    {
        [Display(Name = "Номер ресурса")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}

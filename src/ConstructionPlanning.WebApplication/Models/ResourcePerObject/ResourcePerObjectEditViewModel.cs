using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ResourcePerObject
{
    public class ResourcePerObjectEditViewModel : ResourcePerObjectCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

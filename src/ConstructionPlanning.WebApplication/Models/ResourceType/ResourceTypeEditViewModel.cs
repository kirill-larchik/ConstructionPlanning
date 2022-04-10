using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ResourceType
{
    public class ResourceTypeEditViewModel : ResourceTypeCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

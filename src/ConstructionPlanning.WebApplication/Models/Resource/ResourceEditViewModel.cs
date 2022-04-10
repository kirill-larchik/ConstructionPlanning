using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Resource
{
    public class ResourceEditViewModel : ResourceCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

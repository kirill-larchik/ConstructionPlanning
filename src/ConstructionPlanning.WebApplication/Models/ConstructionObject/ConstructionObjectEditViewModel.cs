using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ConstructionObject
{
    public class ConstructionObjectEditViewModel : ConstructionObjectCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Provider
{
    public class ProviderEditViewModel : ProviderCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

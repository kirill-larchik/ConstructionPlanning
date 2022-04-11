using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Sale
{
    public class SaleEditViewModel : SaleCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

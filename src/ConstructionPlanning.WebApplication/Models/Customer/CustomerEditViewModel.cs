using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Customer
{
    public class CustomerEditViewModel : CustomerCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

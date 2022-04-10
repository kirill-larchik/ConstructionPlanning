using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Delivery
{
    public class DeliveryEditViewModel : DeliveryCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Customer
{
    public class CustomerViewModel
    {
        [Display(Name = "Номер заказчика")]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Информация")]
        public string Description { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

    }
}

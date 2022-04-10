using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Provider
{
    public class ProviderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}

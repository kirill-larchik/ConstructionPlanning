using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.Delivery
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class DeliveryIndexViewModel
    {
        public IEnumerable<DeliveryViewModel> Deliveries { get; set; }

        public DeliveryViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

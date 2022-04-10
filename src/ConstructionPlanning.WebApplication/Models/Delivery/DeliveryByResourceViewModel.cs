namespace ConstructionPlanning.WebApplication.Models.Delivery
{
    /// <summary>
    /// Модель представления страницы "DeliveryByResource".
    /// </summary>
    public class DeliveryByResourceViewModel : DeliveryIndexViewModel
    {
        public int ResourceId { get; set; }

        public string ResourceName { get; set; }
    }
}

namespace ConstructionPlanning.WebApplication.Models.Delivery
{
    /// <summary>
    /// Модель представления страницы "DeliveryByProvider".
    /// </summary>
    public class DeliveryByProviderViewModel : DeliveryIndexViewModel
    {
        public int ProviderId { get; set; }

        public string ProviderName { get; set; }
    }
}

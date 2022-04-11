namespace ConstructionPlanning.WebApplication.Models.Sale
{
    /// <summary>
    /// Модель представления страницы "SaleByResource".
    /// </summary>
    public class SaleByResourceViewModel : SaleIndexViewModel
    {
        public int ResourceId { get; set; }

        public string ResourceName { get; set; }
    }
}

namespace ConstructionPlanning.WebApplication.Models.Resource
{
    /// <summary>
    /// Модель представления страницы "ResourceByType".
    /// </summary>
    public class ResourceByTypeViewModel : ResourceIndexViewModel
    {
        public int ResourceTypeId { get; set; }

        public string ResourceTypeName { get; set; }
    }
}

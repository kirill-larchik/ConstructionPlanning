namespace ConstructionPlanning.WebApplication.Models.Project
{
    /// <summary>
    /// Модель представления страницы "ProjectByType".
    /// </summary>
    public class ProjectByTypeViewModel : ProjectIndexViewModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}

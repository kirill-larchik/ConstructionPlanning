using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.Resource
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class ResourceIndexViewModel
    {
        public IEnumerable<ResourceViewModel> Resources { get; set; }

        public ResourceViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

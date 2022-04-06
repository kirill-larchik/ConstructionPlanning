using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.ResourceType
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class ResourceTypeIndexViewModel
    {
        public IEnumerable<ResourceTypeViewModel> ResourceTypes { get; set; }

        public ResourceTypeViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.Provider
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class ProviderIndexViewModel
    {
        public IEnumerable<ProviderViewModel> Providers { get; set; }

        public ProviderViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

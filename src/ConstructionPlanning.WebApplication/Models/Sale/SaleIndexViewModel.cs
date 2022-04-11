using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.Sale
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class SaleIndexViewModel
    {
        public IEnumerable<SaleViewModel> Sales { get; set; }

        public SaleViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.Customer
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class CustomerIndexViewModel
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public CustomerViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

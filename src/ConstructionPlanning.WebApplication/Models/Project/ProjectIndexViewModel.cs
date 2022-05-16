using System.Collections.Generic;

namespace ConstructionPlanning.WebApplication.Models.Project
{
    /// <summary>
    /// Модель представления страницы "Index".
    /// </summary>
    public class ProjectIndexViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public ProjectViewModel ModelForDisplayingNames { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

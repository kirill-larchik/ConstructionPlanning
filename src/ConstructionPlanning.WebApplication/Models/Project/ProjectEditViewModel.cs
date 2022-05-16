using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.Project
{
    public class ProjectEditViewModel : ProjectCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}

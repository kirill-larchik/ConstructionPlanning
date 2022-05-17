using ConstructionPlanning.WebApplication.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Models.ResourcePerObject
{
    public class ResourcePerObjectCreateViewModel
    {
        [TranslatedRequared]
        public int ConstructionObjectId { get; set; }

        [TranslatedRequared]
        public int ResourceId { get; set; }

        [TranslatedRequared]
        [Display(Name = "Количество ресурсов")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество ресурсов должно быть больше нуля.")]
        public int Count { get; set; }

        [TranslatedRequared]
        public string ReturnUrl { get; set; }

        [Display(Name = "Строительный объект")]
        public string ConstructionObjectName { get; set; }
    }
}

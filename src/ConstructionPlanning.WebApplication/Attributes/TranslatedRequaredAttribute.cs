using System.ComponentModel.DataAnnotations;

namespace ConstructionPlanning.WebApplication.Attributes
{
    /// <inheritdoc />
    public class TranslatedRequaredAttribute : RequiredAttribute
    {
        /// <inheritdoc />
        public TranslatedRequaredAttribute()
        {
            ErrorMessage = "Поле \"{0}\" необходимо заполнить.";
        }
    }
}

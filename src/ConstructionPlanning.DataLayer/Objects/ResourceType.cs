using System.Collections.Generic;

namespace ConstructionPlanning.DataLayer.Objects
{
    /// <summary>
    /// Сущность "Тип ресурса".
    /// </summary>
    public class ResourceType : IBaseObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Наименование типа ресурса.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Описание типа ресурса.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ресурсы данного типа.
        /// </summary>
        public IEnumerable<Resource>? Resources { get; set; }
    }
}

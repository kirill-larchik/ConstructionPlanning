using System.Collections.Generic;

namespace ConstructionPlanning.DataAccess.Objects
{
    /// <summary>
    /// Сущность "Строительный объект".
    /// </summary>
    public class ConstructionObject : IBaseObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Наименование объекта.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// ИД проекта.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Проект.
        /// </summary>
        public Project? Project { get; set; }

        /// <summary>
        /// Список ресурсов на объект.
        /// </summary>
        public IEnumerable<ResourcePerObject>? ResourcesPerObject { get; set; }
    }
}

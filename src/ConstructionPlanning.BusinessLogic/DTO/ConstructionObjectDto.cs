namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Строительный объект".
    /// </summary>
    public class ConstructionObjectDto : IBaseDtoObject
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
        /// Итоговые затраты на объект.
        /// </summary>
        public int TotalCost { get; set; }

        /// <summary>
        /// Проект.
        /// </summary>
        public ProjectDto? Project { get; set; }

        /// <summary>
        /// Список ресурсов на объект.
        /// </summary>
        public IEnumerable<ResourcePerObjectDto>? ResourcesPerObject { get; set; }
    }
}

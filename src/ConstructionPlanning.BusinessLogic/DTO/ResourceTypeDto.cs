namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Тип ресурса".
    /// </summary>
    public class ResourceTypeDto : IBaseDtoObject
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
        public IEnumerable<ResourceDto>? Resources { get; set; }
    }
}

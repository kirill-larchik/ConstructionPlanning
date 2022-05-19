namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>ю
    /// Сущность "Ресурс на объект".
    /// </summary>
    public class ResourcePerObjectDto : IBaseDtoObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// ИД строительного объекта.
        /// </summary>
        public int ConstructionObjectId { get; set; }

        /// <summary>
        /// ИД ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Необходимое количество ресурсов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Общая сумма затрат.
        /// </summary>
        public int TotalCost { get; set; }

        /// <summary>
        /// Разница между ресурсами на складе и необходимым количеством ресурсов.
        /// </summary>
        public int AvaliableResourceCount { get; set; }

        /// <summary>
        /// Строительный объект.
        /// </summary>
        public ConstructionObjectDto? ConstructionObject { get; set; }

        /// <summary>
        /// Ресурс.
        /// </summary>
        public ResourceDto? Resource { get; set; }
    }
}

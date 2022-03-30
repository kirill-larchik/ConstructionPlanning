namespace ConstructionPlanning.DataLayer.Objects
{
    /// <summary>ю
    /// Сущность "Ресурс на объект".
    /// </summary>
    public class ResourcePerObject : IBaseObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// ИД строительного объекта.
        /// </summary>
        public int ConstructionObjectId { get; set; }

        /// <summary>
        /// Строительный объект.
        /// </summary>
        public ConstructionObject? ConstructionObject { get; set; }

        /// <summary>
        /// ИД ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Ресурс.
        /// </summary>
        public Resource? Resource { get; set; }

        /// <summary>
        /// Необходимое количество ресурсов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Необходимое используемых ресурсов.
        /// </summary>
        public int UsedCount { get; set; }

        /// <summary>
        /// Общая сумма затрат.
        /// </summary>
        public int TotalCost { get; set; }
    }
}

namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Продажа".
    /// </summary>
    public class SaleDto : IBaseDtoObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Дата продажи.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ИД ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Ресурс.
        /// </summary>
        public ResourceDto? Resource { get; set; }

        /// <summary>
        /// Покупатель.
        /// </summary>
        public string? Customer { get; set; }

        /// <summary>
        /// Количество проданных ресурсов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Общая стоимость продажи.
        /// </summary>
        public int TotalCost { get; set; }
    }
}

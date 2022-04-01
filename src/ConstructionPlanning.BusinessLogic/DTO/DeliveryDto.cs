namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Поставка".
    /// </summary>
    public class DeliveryDto : IBaseDtoObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// ИД ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Ресурс.
        /// </summary>
        public ResourceDto? Resource { get; set; }

        /// <summary>
        /// ИД поставщика.
        /// </summary>
        public int ProviderId { get; set; }

        /// <summary>
        /// Поставщик.
        /// </summary>
        public ProviderDto? Provider { get; set; }

        /// <summary>
        /// Дата поставки.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Количество ресурсов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Стоимость единицы ресурса.
        /// </summary>
        public int UnitCost { get; set; }

        /// <summary>
        /// Общая стоимость поставки.
        /// </summary>
        public int Cost { get; set; }
    }
}

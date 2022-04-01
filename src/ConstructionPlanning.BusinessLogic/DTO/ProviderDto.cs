﻿namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Поставшик".
    /// </summary>
    public class ProviderDto : IBaseDtoObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Наименование поставщика.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Адрес поставшика.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Номер поставщика.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Отображает статус отображения ресурса.
        /// Если значение 'true', то ресурс отображается на странице пользователя.
        /// Если знчение 'false' - не отображается на странице пользователя.
        /// </summary>
        public bool IsVisivle { get; set; }

        /// <summary>
        /// Поставки поставщика.
        /// </summary>
        public IEnumerable<DeliveryDto>? Deliveries { get; set; }
    }
}

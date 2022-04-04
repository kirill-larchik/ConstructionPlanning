namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Ресурс".
    /// </summary>
    public class ResourceDto : IBaseDtoObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// ИД типа ресурса.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Тип ресурса.
        /// </summary>
        public ResourceTypeDto? Type { get; set; }

        /// <summary>
        /// Доступное количество ресурса на складе.
        /// </summary>
        public int AvaliableAmount { get; set; }

        /// <summary>
        /// Стоимость единицы ресурса.
        /// </summary>
        public int UnitCost { get; set; }

        /// <summary>
        /// Отображает статус отображения ресурса.
        /// Если значение 'true', то ресурс отображается на странице пользователя.
        /// Если знчение 'false' - не отображается на странице пользователя.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Список всех продаж по данному ресурсу.
        /// </summary>
        public IEnumerable<SaleDto>? Sales { get; set; }

        /// <summary>
        /// Список всех поставок по данному ресурсу.
        /// </summary>
        public IEnumerable<DeliveryDto>? Deliveries { get; set; }

        /// <summary>
        /// Список всех объектов по данному ресурсу.
        /// </summary>
        public IEnumerable<ResourcePerObjectDto>? ResourcesPerObject { get; set; }
    }
}

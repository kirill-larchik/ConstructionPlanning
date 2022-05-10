using System.Collections.Generic;

namespace ConstructionPlanning.DataAccess.Objects
{
    /// <summary>
    /// Сущность "Ресурс".
    /// </summary>
    public class Resource : IBaseObject
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
        public int? TypeId { get; set; }

        /// <summary>
        /// Тип ресурса.
        /// </summary>
        public ResourceType? Type { get; set; }

        /// <summary>
        /// Доступное количество ресурса на складе.
        /// </summary>
        public int AvaliableAmount { get; set; }

        /// <summary>
        /// Стоимость единицы ресурса.
        /// </summary>
        public int UnitCost { get; set; }

        /// <summary>
        /// Список всех продаж по данному ресурсу.
        /// </summary>
        public IEnumerable<Sale>? Sales { get; set; }

        /// <summary>
        /// Список всех поставок по данному ресурсу.
        /// </summary>
        public IEnumerable<Delivery>? Deliveries { get; set; }

        /// <summary>
        /// Список всех объектов по данному ресурсу.
        /// </summary>
        public IEnumerable<ResourcePerObject>? ResourcesPerObject { get; set; }
    }
}

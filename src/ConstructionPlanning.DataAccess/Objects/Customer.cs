using System.Collections.Generic;

namespace ConstructionPlanning.DataAccess.Objects
{
    /// <summary>
    /// Сущность "Заказчик".
    /// </summary>
    public class Customer : IBaseObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Имя заказчика.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Дополнительная информация о заказчике.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Номер заказчика.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Список проектов заказчика. 
        /// </summary>
        public IEnumerable<Project>? Projects { get; set; }
    }
}

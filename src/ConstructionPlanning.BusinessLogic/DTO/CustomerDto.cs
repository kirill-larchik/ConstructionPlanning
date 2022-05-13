namespace ConstructionPlanning.BusinessLogic.DTO
{
    /// <summary>
    /// Сущность "Заказчик".
    /// </summary>
    public class CustomerDto : IBaseDtoObject
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
        public IEnumerable<ProjectDto>? Projects { get; set; }
    }
}

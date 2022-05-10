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
        /// Имя поставщика.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия поставщика.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Отчество поставщика.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Номер поставщика.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Наименование организации.
        /// </summary>
        public string? Organization { get; set; }

        /// <summary>
        /// Адрес организации.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Список проектов заказчика. 
        /// </summary>
        public IEnumerable<ProjectDto>? Projects { get; set; }
    }
}

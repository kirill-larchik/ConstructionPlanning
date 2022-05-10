using System;
using System.Collections.Generic;

namespace ConstructionPlanning.DataAccess.Objects
{
    /// <summary>
    /// Сущность "Проект".
    /// </summary>
    public class Project : IBaseObject
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Наименование проекта.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Дата создания проекта.
        /// </summary>
        public DateTime DateOfCreate { get; set; }

        /// <summary>
        /// Крайний срок выполнения.
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Выделение средства.
        /// </summary>
        public int AllocatedAmount { get; set; }

        /// <summary>
        /// Текущее количество средств.
        /// </summary>
        public int CurrentAmount { get; set; }

        /// <summary>
        /// Общая сумма затрат.
        /// </summary>
        public int TotalCost { get; set; }

        /// <summary>
        /// ИД заказчика.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Заказчик.
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// Строительные объекты.
        /// </summary>
        public IEnumerable<ConstructionObject>? ConstructionObjects { get; set; }
    }
}

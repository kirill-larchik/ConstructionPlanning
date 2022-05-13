using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с заказчиками.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Вовзращает все проекты.
        /// </summary>
        Task<IEnumerable<CustomerDto>> GetAllCustomers();

        /// <summary>
        /// Возвращает всех заказчиков с применением пагинации.
        /// </summary>
        Task<IEnumerable<CustomerDto>> GetAllPaginatedCustomers(int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех заказчиков.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Вовзращает заказчика по ИД.
        /// </summary>
        Task<CustomerDto> GetCustomerById(int id);

        /// <summary>
        /// Добавляет нового заказчика.
        /// </summary>
        Task AddCustomer(CustomerDto projectDto);

        /// <summary>
        /// Обновляет данные заказчика.
        /// </summary>
        Task UpdateCustomer(CustomerDto projectDto);

        /// <summary>
        /// Удаляет заказчика по заданному ИД.
        /// </summary>
        Task DeleteCustomerById(int id);
    }
}
using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с поставками.
    /// </summary>
    public interface IDeliveryService
    {
        /// <summary>
        /// Вовзращает все поставки.
        /// </summary>
        Task<IEnumerable<DeliveryDto>> GetAllDeliveries();

        /// <summary>
        /// Возвращает все поставки с применением пагинации.
        /// </summary>
        Task<IEnumerable<DeliveryDto>> GetAllPaginatedDeliveries(int page, int pageSize);

        /// <summary>
        /// Возвращает все поставки по заказчику с применением пагинации.
        /// </summary>
        Task<IEnumerable<DeliveryDto>> GetAllPaginatedDeliveriesByProviderId(int providerId, int page, int pageSize);

        /// <summary>
        /// Возвращает все поставки по ресурсу с применением пагинации.
        /// </summary>
        Task<IEnumerable<DeliveryDto>> GetAllPaginatedDeliveriesByResourceId(int resourceId, int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех поставок.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Возвращает общее количество всех поставок по заказчику.
        /// </summary>
        Task<int> GetTotalCountByProviderId(int providerId);

        /// <summary>
        /// Возвращает общее количество всех поставок по ресурсу.
        /// </summary>
        Task<int> GetTotalCountByResourceId(int resourceId);

        /// <summary>
        /// Вовзращает поставку по заданному ИД.
        /// </summary>
        Task<DeliveryDto> GetDeliveryById(int id);

        /// <summary>
        /// Добавляет новую поставку.
        /// </summary>
        Task AddDelivery(DeliveryDto deliveryDto);

        /// <summary>
        /// Обновляет поставку.
        /// </summary>
        Task UpdateDelivery(DeliveryDto deliveryDto);

        /// <summary>
        /// Удаляет поставку по заданному ИД.
        /// </summary>
        Task DeleteDeliveryById(int id);
    }
}

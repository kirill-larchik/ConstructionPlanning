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
        Task<IEnumerable<DeliveryDto>> GetAllDeliveriesByPagination(int page, int pageSize);

        /// <summary>
        /// Возвращает все поставки по заказчику с применением пагинации.
        /// </summary>
        Task<IEnumerable<DeliveryDto>> GetAllDeliveriesByProviderIdWithPagination(int providerId, int page, int pageSize);

        /// <summary>
        /// Возвращает все поставки по ресурсу с применением пагинации.
        /// </summary>
        Task<IEnumerable<DeliveryDto>> GetAllDeliveriesByResourceIdWithPagination(int resourceId, int page, int pageSize);

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
        /// Вовзращает ресурс по заданному условию.
        /// </summary>
        Task<DeliveryDto> GetDelivery(Func<DeliveryDto, bool> predicate);

        /// <summary>
        /// Добавляет новый ресурс.
        /// </summary>
        Task AddDelivery(DeliveryDto deliveryDto);

        /// <summary>
        /// Обновляет ресурс.
        /// </summary>
        Task UpdateDelivery(DeliveryDto deliveryDto);

        /// <summary>
        /// Удаляет ресурс по заданному ИД.
        /// </summary>
        Task DeleteDeliveryById(int id);
    }
}

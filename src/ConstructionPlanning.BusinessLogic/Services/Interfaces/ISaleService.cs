using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с продажами.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Вовзращает все продажи.
        /// </summary>
        Task<IEnumerable<SaleDto>> GetAllSales();

        /// <summary>
        /// Возвращает все продажи с применением пагинации.
        /// </summary>
        Task<IEnumerable<SaleDto>> GetAllPaginatedSales(int page, int pageSize);

        /// <summary>
        /// Возвращает все продажи ресурса с применением пагинации.
        /// </summary>
        Task<IEnumerable<SaleDto>> GetAllPaginatedSalesByResourceId(int resourceId, int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех продаж.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Возвращает общее количество всех продаж по ресурсу.
        /// </summary>
        Task<int> GetTotalCountByResourceId(int resourceId);

        /// <summary>
        /// Вовзращает продажу по заданному ИД.
        /// </summary>
        Task<SaleDto> GetSaleById(int id);

        /// <summary>
        /// Добавляет новую продажу.
        /// </summary>
        Task AddSale(SaleDto deliveryDto);

        /// <summary>
        /// Обновляет продажу.
        /// </summary>
        Task UpdateSale(SaleDto deliveryDto);

        /// <summary>
        /// Удаляет продажу по заданному ИД.
        /// </summary>
        Task DeleteSaleById(int id);
    }
}

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
        Task<IEnumerable<SaleDto>> GetAllSalesByPagination(int page, int pageSize);

        /// <summary>
        /// Возвращает все продажи ресурса с применением пагинации.
        /// </summary>
        Task<IEnumerable<SaleDto>> GetAllSalesByResourceIdWithPagination(int resourceId, int page, int pageSize);

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
        /// Вовзращает продажу по заданному условию.
        /// </summary>
        Task<SaleDto> GetSale(Func<SaleDto, bool> predicate);

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

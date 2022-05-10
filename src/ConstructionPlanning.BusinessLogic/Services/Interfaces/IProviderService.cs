using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с поставщиками.
    /// </summary>
    public interface IProviderService
    {
        /// <summary>
        /// Вовзращает всех поставщиков.
        /// </summary>
        Task<IEnumerable<ProviderDto>> GetAllProviders();

        /// <summary>
        /// Возвращает всех поставщиков с применением пагинации.
        /// </summary>
        Task<IEnumerable<ProviderDto>> GetAllPaginatedProviders(int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех поставщиков.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Вовзращает всех поставщиков по ИД.
        /// </summary>
        Task<ProviderDto> GetProviderById(int id);

        /// <summary>
        /// Добавляет нового поставщика.
        /// </summary>
        Task AddProvider(ProviderDto providerDto);

        /// <summary>
        /// Обновляет поставщика.
        /// </summary>
        Task UpdateProvider(ProviderDto providerDto);

        /// <summary>
        /// Удаляет поставщика по заданному ИД.
        /// </summary>
        Task DeleteProviderById(int id);
    }
}

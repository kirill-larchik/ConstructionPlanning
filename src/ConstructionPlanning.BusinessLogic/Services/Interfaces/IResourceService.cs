using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с ресурсами.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Вовзращает все ресурсы.
        /// </summary>
        Task<IEnumerable<ResourceDto>> GetAllResources();

        /// <summary>
        /// Возвращает все ресурсы с применением пагинации.
        /// </summary>
        Task<IEnumerable<ResourceDto>> GetAllResourcesByPagination(int page, int pageSize);

        /// <summary>
        /// Возвращает все ресурсы по типу ресурса с применением пагинации.
        /// </summary>
        Task<IEnumerable<ResourceDto>> GetAllResourcesByResourceTypeIdWithPagination(int resourceTypeId, int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех ресурсов.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Возвращает общее количество всех ресурсов по типу ресурса.
        /// </summary>
        Task<int> GetTotalCountByResourceTypeId(int resourceTypeId);

        /// <summary>
        /// Вовзращает ресурс по заданному условию.
        /// </summary>
        Task<ResourceDto> GetResourceById(int id);

        /// <summary>
        /// Вовзращает ресурс по заданному условию.
        /// </summary>
        Task<ResourceDto> GetResource(Func<ResourceDto, bool> predicate);

        /// <summary>
        /// Добавляет новый ресурс.
        /// </summary>
        Task AddResource(ResourceDto resourceDto);

        /// <summary>
        /// Обновляет ресурс.
        /// </summary>
        Task UpdateResource(ResourceDto resourceDto);

        /// <summary>
        /// Удаляет ресурс по заданному ИД.
        /// </summary>
        Task DeleteResourceById(int id);
    }
}

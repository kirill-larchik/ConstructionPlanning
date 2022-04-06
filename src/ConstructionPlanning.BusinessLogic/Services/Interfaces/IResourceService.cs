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
        Task<IEnumerable<ResourceDto>> GetAllResourcesByPageAndPageSize(int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех ресурсов.
        /// </summary>
        Task<int> GetTotalCount();

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

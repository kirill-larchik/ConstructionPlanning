using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с типами ресурсов.
    /// </summary>
    public interface IResourceTypeService
    {
        /// <summary>
        /// Вовзращает все типы ресурсы.
        /// </summary>
        Task<IEnumerable<ResourceTypeDto>> GetAllResourceTypes();

        /// <summary>
        /// Возвращает все ресурсы с применением пагинации.
        /// </summary>
        Task<IEnumerable<ResourceTypeDto>> GetAllResourcesByPageAndPageSize(int page, int pageSize);

        /// <summary>
        /// Вовзращает тип ресурса по заданному условию.
        /// </summary>
        Task<ResourceTypeDto> GetResourceTypeById(int id);

        /// <summary>
        /// Возвращает общее количество всех ресурсов.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Вовзращает тип ресурса по заданному условию.
        /// </summary>
        Task<ResourceTypeDto?> GetResourceType(Func<ResourceTypeDto, bool> predicate);

        /// <summary>
        /// Добавляет новый тип ресурса.
        /// </summary>
        Task AddResourceType(ResourceTypeDto resourceDto);

        /// <summary>
        /// Обновляет тип ресурса.
        /// </summary>
        Task UpdateResourceType(ResourceTypeDto resourceDto);

        /// <summary>
        /// Удаляет тип ресурса по заданному ИД.
        /// </summary>
        Task DeleteResourceTypeById(int id);
    }
}

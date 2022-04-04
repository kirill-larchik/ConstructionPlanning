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
        /// Вовзращает тип ресурса по заданному условию.
        /// </summary>
        Task<ResourceTypeDto> GetResourceTypeById(int id);

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

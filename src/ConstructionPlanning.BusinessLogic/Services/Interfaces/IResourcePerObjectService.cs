using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с ресурсами для объекта.
    /// </summary>
    public interface IResourcePerObjectService
    {
        /// <summary>
        /// Вовзращает все ресурсы для объектов.
        /// </summary>
        Task<IEnumerable<ResourcePerObjectDto>> GetAllResourcePerObjects();

        /// <summary>
        /// Возвращает все ресурсы для объектов с применением пагинации.
        /// </summary>
        Task<IEnumerable<ResourcePerObjectDto>> GetAllPaginatedResourcePerObjects(int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех ресурсов для объектов.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Вовзращает все ресурсов для объектов по ИД.
        /// </summary>
        Task<ResourcePerObjectDto> GetResourcePerObjectById(int id);

        /// <summary>
        /// Добавляет новый ресурс для объекта.
        /// </summary>
        Task AddResourcePerObject(ResourcePerObjectDto ResourcePerObjectDto);

        /// <summary>
        /// Обновляет ресурс для объекта.
        /// </summary>
        Task UpdateResourcePerObject(ResourcePerObjectDto ResourcePerObjectDto);

        /// <summary>
        /// Удаляет ресурс для объекта по заданному ИД.
        /// </summary>
        Task DeleteResourcePerObjectById(int id);
    }
}

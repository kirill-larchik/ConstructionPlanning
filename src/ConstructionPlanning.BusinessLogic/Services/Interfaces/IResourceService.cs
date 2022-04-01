using ConstructionPlanning.BusinessLogic.DTO;
using System.Linq.Expressions;

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

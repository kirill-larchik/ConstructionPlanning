using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с строительными объектами.
    /// </summary>
    public interface IConstructionObjectService
    {
        /// <summary>
        /// Вовзращает все строительные объекты.
        /// </summary>
        Task<IEnumerable<ConstructionObjectDto>> GetAllConstructionObjects();

        /// <summary>
        /// Возвращает все строительные объекты с применением пагинации.
        /// </summary>
        Task<IEnumerable<ConstructionObjectDto>> GetAllPaginatedConstructionObjects(int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех строительныех объектов.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Вовзращает строительный объект по ИД.
        /// </summary>
        Task<ConstructionObjectDto> GetConstructionObjectById(int id);

        /// <summary>
        /// Добавляет новый строительный объект.
        /// </summary>
        Task AddConstructionObject(ConstructionObjectDto constructionObjectDto);

        /// <summary>
        /// Обновляет строительный объект.
        /// </summary>
        Task UpdateConstructionObject(ConstructionObjectDto constructionObjectDto);

        /// <summary>
        /// Удаляет строительный объект по заданному ИД.
        /// </summary>
        Task DeleteConstructionObjectById(int id);
    }
}

using ConstructionPlanning.BusinessLogic.DTO;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с проектами.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Вовзращает все проекты.
        /// </summary>
        Task<IEnumerable<ProjectDto>> GetAllProjects();

        /// <summary>
        /// Возвращает все проекты с применением пагинации.
        /// </summary>
        Task<IEnumerable<ProjectDto>> GetAllPaginatedProjects(int page, int pageSize);

        /// <summary>
        /// Возвращает все ресурсы по типу ресурса с применением пагинации.
        /// </summary>
        Task<IEnumerable<ProjectDto>> GetAllPaginatedProjectsByCustomerId(int customerId, int page, int pageSize);

        /// <summary>
        /// Возвращает общее количество всех ресурсов.
        /// </summary>
        Task<int> GetTotalCount();

        /// <summary>
        /// Возвращает общее количество всех ресурсов по типу ресурса.
        /// </summary>
        Task<int> GetTotalCountByCustomerId(int customerId);

        /// <summary>
        /// Вовзращает проект по ИД.
        /// </summary>
        Task<ProjectDto> GetProjectById(int id);

        /// <summary>
        /// Добавляет новый проект.
        /// </summary>
        Task AddProject(ProjectDto projectDto);

        /// <summary>
        /// Обновляет проект.
        /// </summary>
        Task UpdateProject(ProjectDto projectDto);

        /// <summary>
        /// Удаляет проект по заданному ИД.
        /// </summary>
        Task DeleteProjectById(int id);
    }
}
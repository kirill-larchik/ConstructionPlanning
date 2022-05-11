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
        /// Возвращает общее количество всех проектов.
        /// </summary>
        Task<int> GetTotalCount();

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
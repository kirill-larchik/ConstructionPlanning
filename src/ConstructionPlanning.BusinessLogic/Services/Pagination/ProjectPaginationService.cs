using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class ProjectPaginationService : BasePaginationService<Project, ProjectDto>
    {
        /// <inheritdoc />
        public ProjectPaginationService(IRepository<Project> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

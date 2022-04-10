using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class ResourcePaginationService : BasePaginationService<Resource, ResourceDto>
    {
        /// <inheritdoc />
        public ResourcePaginationService(IRepository<Resource> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}

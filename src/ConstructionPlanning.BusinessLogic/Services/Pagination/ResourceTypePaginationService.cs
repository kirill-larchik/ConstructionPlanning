using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class ResourceTypePaginationService : BasePaginationService<ResourceType, ResourceTypeDto>
    {
        /// <inheritdoc />
        public ResourceTypePaginationService(IRepository<ResourceType> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}

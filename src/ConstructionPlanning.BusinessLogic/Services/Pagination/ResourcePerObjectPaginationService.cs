using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class ResourcePerObjectPaginationService : BasePaginationService<ResourcePerObject, ResourcePerObjectDto>
    {
        /// <inheritdoc />
        public ResourcePerObjectPaginationService(IRepository<ResourcePerObject> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

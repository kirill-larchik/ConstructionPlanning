using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class ProviderPaginationService : BasePaginationService<Provider, ProviderDto>
    {
        /// <inheritdoc />
        public ProviderPaginationService(IRepository<Provider> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

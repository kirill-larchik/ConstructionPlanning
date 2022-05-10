using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class ConstructionObjectPaginationService : BasePaginationService<ConstructionObject, ConstructionObjectDto>
    {
        /// <inheritdoc />
        public ConstructionObjectPaginationService(IRepository<ConstructionObject> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

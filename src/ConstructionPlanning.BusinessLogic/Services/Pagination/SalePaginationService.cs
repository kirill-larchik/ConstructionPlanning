using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class SalePaginationService : BasePaginationService<Sale, SaleDto>
    {
        /// <inheritdoc />
        public SalePaginationService(IRepository<Sale> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}

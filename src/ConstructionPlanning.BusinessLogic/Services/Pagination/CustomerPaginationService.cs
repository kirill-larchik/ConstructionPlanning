using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class CustomerPaginationService : BasePaginationService<Customer, CustomerDto>
    {
        /// <inheritdoc />
        public CustomerPaginationService(IRepository<Customer> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

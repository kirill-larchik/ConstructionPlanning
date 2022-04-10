using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class DeliveryPaginationService : BasePaginationService<Delivery, DeliveryDto>
    {
        /// <inheritdoc />
        public DeliveryPaginationService(IRepository<Delivery> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}

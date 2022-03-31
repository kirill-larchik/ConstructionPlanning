using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class DeliveryRepository : BaseRepository<Delivery>
    {
        /// <inheritdoc />
        public DeliveryRepository(ConstructionPlanningDbContext context) 
            : base(context)
        {
        }
    }
}

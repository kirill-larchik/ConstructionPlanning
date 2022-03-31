using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class SaleRepository : BaseRepository<Sale>
    {
        /// <inheritdoc />
        public SaleRepository(ConstructionPlanningDbContext context) 
            : base(context)
        {
        }
    }
}

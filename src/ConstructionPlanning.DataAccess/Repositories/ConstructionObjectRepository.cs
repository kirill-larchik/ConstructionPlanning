using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class ConstructionObjectRepository : BaseRepository<ConstructionObject>
    {
        /// <inheritdoc />
        public ConstructionObjectRepository(ConstructionPlanningDbContext context) 
            : base(context)
        {
        }
    }
}

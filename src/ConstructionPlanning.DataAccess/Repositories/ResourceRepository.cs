using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class ResourceRepository : BaseRepository<Resource>
    {
        /// <inheritdoc />
        public ResourceRepository(ConstructionPlanningDbContext context)
            : base(context)
        {
        }
    }
}

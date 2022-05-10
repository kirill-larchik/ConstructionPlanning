using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    internal class ResourcePerObjectRepository : BaseRepository<ResourcePerObject>
    {
        /// <inheritdoc />
        public ResourcePerObjectRepository(ConstructionPlanningDbContext context)
            : base(context)
        {
        }
    }
}

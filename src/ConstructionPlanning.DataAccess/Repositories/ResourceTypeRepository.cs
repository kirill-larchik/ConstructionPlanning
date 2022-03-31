using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class ResourceTypeRepository : BaseRepository<ResourceType>
    {
        /// <inheritdoc />
        public ResourceTypeRepository(ConstructionPlanningDbContext context) 
            : base(context)
        {
        }
    }
}

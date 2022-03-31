using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class ProviderRepository : BaseRepository<Provider>
    {
        /// <inheritdoc />
        public ProviderRepository(ConstructionPlanningDbContext context)
            : base(context)
        {
        }
    }
}

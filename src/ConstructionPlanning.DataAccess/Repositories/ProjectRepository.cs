using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class ProjectRepository : BaseRepository<Project>
    {
        /// <inheritdoc />
        public ProjectRepository(ConstructionPlanningDbContext context)
            : base(context)
        {
        }
    }
}

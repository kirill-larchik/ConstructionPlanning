using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class CustomerRepository : BaseRepository<Customer>
    {
        /// <inheritdoc />
        public CustomerRepository(ConstructionPlanningDbContext context) 
            : base(context)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.DataAccess.DbContext
{
    /// <summary>
    /// Контекст данных БД "ConstructionPlanningDb".
    /// </summary>
    public class ConstructionPlanningDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Provider>? Providers { get; set; }

        public DbSet<Delivery>? Deliveries { get; set; }

        public DbSet<Resource>? Resources { get; set; }

        public DbSet<ResourceType>? ResourceTypes { get; set; }

        public DbSet<Sale>? Sales { get; set; }

        /// <inheritdoc />
        public ConstructionPlanningDbContext(DbContextOptions<ConstructionPlanningDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

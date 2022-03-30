using Microsoft.EntityFrameworkCore;
using ConstructionPlanning.DataLayer.Objects;

namespace ConstructionPlanning.DataLayer.DbContext
{
    /// <summary>
    /// Контекст данных БД "ConstructionPlanningDb".
    /// </summary>
    public class ConstructionPlanningDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<ConstructionObject> ConstructionObjects { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourcePerObject> ResourcesPerObject { get; set; }

        public DbSet<ResourceType> ResourceTypes { get; set; }

        public DbSet<Sale> Sales { get; set; }
    }
}

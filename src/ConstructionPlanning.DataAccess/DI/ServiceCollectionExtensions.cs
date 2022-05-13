using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionPlanning.DataAccess.DI
{
    /// <summary>
    /// Содержит метод для внедерния слоя доступа к данным.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет сервисы доступа к данным.
        /// </summary>
        public static void AddDataAccesServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Resource>, ResourceRepository>();
            services.AddTransient<IRepository<ResourceType>, ResourceTypeRepository>();
            services.AddTransient<IRepository<Provider>, ProviderRepository>();
            services.AddTransient<IRepository<Delivery>, DeliveryRepository>();
            services.AddTransient<IRepository<Sale>, SaleRepository>();
            services.AddTransient<IRepository<ResourcePerObject>, ResourcePerObjectRepository>();
            services.AddTransient<IRepository<ConstructionObject>, ConstructionObjectRepository>();
            services.AddTransient<IRepository<Project>, ProjectRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
        }
    }
}

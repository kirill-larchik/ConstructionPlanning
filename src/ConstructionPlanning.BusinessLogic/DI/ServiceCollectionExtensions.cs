using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services;
using ConstructionPlanning.BusinessLogic.Services.Export;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.BusinessLogic.Services.Pagination;
using ConstructionPlanning.DataAccess.DI;
using ConstructionPlanning.DataAccess.Objects;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionPlanning.BusinessLogic.DI
{
    /// <summary>
    /// Содержит метод для внедерния слоя доступа к данным.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет сервисы доступа к данным.
        /// </summary>
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddDataAccesServices();
            AddServices(services);
            AddPaginationServices(services);
            AddExportServices(services);
        }

        private static void AddExportServices(IServiceCollection services)
        {
            services.AddTransient<IExcelExportService<DeliveryDto>, DeliveryExportService>();
            services.AddTransient<IExcelExportService<SaleDto>, SaleExportService>();
            services.AddTransient<IExcelExportService<ProjectDto>, ProjectExportService>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IResourceTypeService, ResourceTypeService>();
            services.AddTransient<IResourceService, ResourceService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IResourcePerObjectService, ResourcePerObjectService>();
            services.AddTransient<IConstructionObjectService, ConstructionObjectService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ICustomerService, CustomerService>();
        }

        private static void AddPaginationServices(IServiceCollection services)
        {
            services.AddTransient<IPaginationService<Resource, ResourceDto>, ResourcePaginationService>();
            services.AddTransient<IPaginationService<ResourceType, ResourceTypeDto>, ResourceTypePaginationService>();
            services.AddTransient<IPaginationService<Provider, ProviderDto>, ProviderPaginationService>();
            services.AddTransient<IPaginationService<Delivery, DeliveryDto>, DeliveryPaginationService>();
            services.AddTransient<IPaginationService<Sale, SaleDto>, SalePaginationService>();
            services.AddTransient<IPaginationService<ResourcePerObject, ResourcePerObjectDto>, ResourcePerObjectPaginationService>();
            services.AddTransient<IPaginationService<ConstructionObject, ConstructionObjectDto>, ConstructionObjectPaginationService>();
            services.AddTransient<IPaginationService<Project, ProjectDto>, ProjectPaginationService>();
            services.AddTransient<IPaginationService<Customer, CustomerDto>, CustomerPaginationService>();
        }
    }
}

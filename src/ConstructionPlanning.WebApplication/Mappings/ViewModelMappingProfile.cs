using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models;
using ConstructionPlanning.WebApplication.Models.Customer;
using ConstructionPlanning.WebApplication.Models.Delivery;
using ConstructionPlanning.WebApplication.Models.Project;
using ConstructionPlanning.WebApplication.Models.Provider;
using ConstructionPlanning.WebApplication.Models.Resource;
using ConstructionPlanning.WebApplication.Models.ResourceType;
using ConstructionPlanning.WebApplication.Models.Sale;

namespace ConstructionPlanning.WebApplication.Mappings
{
    /// <summary>
    /// Производит маппинг объектов передачи данных и моделей представления.
    /// </summary>
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            CreateMap<ResourceDto, ResourceViewModel>()
                .ForMember(x => x.TypeName, y => y.MapFrom(src => src.Type != null ? src.Type.Name : Constants.NoInfoString))
                .ReverseMap();
            CreateMap<ResourceCreateViewModel, ResourceDto>().ReverseMap();
            CreateMap<ResourceEditViewModel, ResourceDto>().ReverseMap();
            CreateMap<SelectListModel, ResourceTypeDto>().ReverseMap();

            CreateMap<ResourceTypeDto, ResourceTypeViewModel>().ReverseMap();
            CreateMap<ResourceTypeCreateViewModel, ResourceTypeDto>().ReverseMap();
            CreateMap<ResourceTypeEditViewModel, ResourceTypeDto>().ReverseMap();

            CreateMap<ProviderDto, ProviderViewModel>().ReverseMap();
            CreateMap<ProviderCreateViewModel, ProviderDto>().ReverseMap();
            CreateMap<ProviderEditViewModel, ProviderDto>().ReverseMap();

            CreateMap<DeliveryDto, DeliveryViewModel>()
                .ForMember(x => x.ResourceName, y => y.MapFrom(src => src.Resource != null ? src.Resource.Name : Constants.NoInfoString))
                .ForMember(x => x.ProviderName, y => y.MapFrom(src => src.Provider != null ? src.Provider.Name : Constants.NoInfoString))
                .ReverseMap();
            CreateMap<DeliveryCreateViewModel, DeliveryDto>().ReverseMap();
            CreateMap<DeliveryEditViewModel, DeliveryDto>().ReverseMap();
            CreateMap<SelectListModel, ResourceDto>().ReverseMap();
            CreateMap<SelectListModel, ProviderDto>().ReverseMap();

            CreateMap<SaleDto, SaleViewModel>()
                .ForMember(x => x.ResourceName, y => y.MapFrom(src => src.Resource != null ? src.Resource.Name : Constants.NoInfoString))
                .ForMember(x => x.UnitCost, y => y.MapFrom(src => src.Resource == null ? 0 : src.Resource.UnitCost))
                .ReverseMap();
            CreateMap<SaleCreateViewModel, SaleDto>().ReverseMap();
            CreateMap<SaleEditViewModel, SaleDto>().ReverseMap();

            CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();
            CreateMap<CustomerCreateViewModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerEditViewModel, CustomerDto>().ReverseMap();

            CreateMap<ProjectDto, ProjectViewModel>()
                .ForMember(x => x.CustomerName, y => y.MapFrom(src => src.Customer != null ? src.Customer.Name : Constants.NoInfoString))
                .ReverseMap();
            CreateMap<ProjectCreateViewModel, ProjectDto>().ReverseMap();
            CreateMap<ProjectEditViewModel, ProjectDto>().ReverseMap();
            CreateMap<SelectListModel, CustomerDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.DataAccess.Objects;

namespace ConstructionPlanning.BusinessLogic.Mappings
{
    /// <summary>
    /// Производит маппинг объектов и объектов передачи данных (дто).
    /// </summary>
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<ResourceType, ResourceTypeDto>().ReverseMap();

            CreateMap<Delivery, DeliveryDto>()
                .ForMember(x => x.TotalCost, y => y.MapFrom(src => src.UnitCost * src.Count));
            CreateMap<DeliveryDto, Delivery>();

            CreateMap<Sale, SaleDto>()
                .ForMember(x => x.TotalCost, y => y.MapFrom(src => GetUnitCostForResource(src.Resource) * src.Count));
            CreateMap<SaleDto, Sale>();

            CreateMap<ResourcePerObject, ResourcePerObjectDto>()
                .ForMember(x => x.TotalCost, y => y.MapFrom(src => GetUnitCostForResource(src.Resource) * src.Count))
                .ForMember(x => x.ResourceCountOffset, y => y.MapFrom(src => GetAvaliableAmountForResource(src.Resource) - src.Count));
            CreateMap<ResourcePerObjectDto, ResourcePerObject>();

            CreateMap<ConstructionObject, ConstructionObjectDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }

        private static int GetUnitCostForResource(Resource? resource)
        {
            return resource == null ? 0 : resource.UnitCost;
        }

        private static int GetAvaliableAmountForResource(Resource? resource)
        {
            return resource == null ? 0 : resource.AvaliableAmount;
        }
    }
}

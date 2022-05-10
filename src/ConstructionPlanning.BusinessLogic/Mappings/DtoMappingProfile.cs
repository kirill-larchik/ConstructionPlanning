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
            CreateMap<Delivery, DeliveryDto>()
                .ForMember(x => x.TotalCost, y => y.MapFrom(src => src.UnitCost * src.Count));
            CreateMap<DeliveryDto, Delivery>();

            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<ResourceType, ResourceTypeDto>().ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(x => x.TotalCost, y => y.MapFrom(src => (src.Resource == null ? 0 : src.Resource.UnitCost) * src.Count));
            CreateMap<SaleDto, Sale>();
        }
    }
}

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
            CreateMap<Delivery, DeliveryDto>().ReverseMap();
            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<ResourceType, ResourceTypeDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
        }
    }
}

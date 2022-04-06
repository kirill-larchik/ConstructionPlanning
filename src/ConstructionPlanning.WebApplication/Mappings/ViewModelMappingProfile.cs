using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.WebApplication.Models.Resource;
using ConstructionPlanning.WebApplication.Models.ResourceType;

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
                .ForMember(x => x.TypeName, y => y.MapFrom(src => src.Type.Name))
                .ReverseMap();
            CreateMap<ResourceCreateViewModel, ResourceDto>().ReverseMap();
            CreateMap<ResourceEditViewModel, ResourceDto>().ReverseMap();
            CreateMap<ResourceTypeSelectListModel, ResourceTypeDto>().ReverseMap();

            CreateMap<ResourceTypeDto, ResourceTypeViewModel>().ReverseMap();
            CreateMap<ResourceTypeCreateViewModel, ResourceTypeDto>().ReverseMap();
            CreateMap<ResourceTypeEditViewModel, ResourceTypeDto>().ReverseMap();
        }
    }
}

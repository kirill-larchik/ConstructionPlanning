using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services
{
    //TODO
    /// <inheritdoc />
    public class ResourceTypeService : IResourceTypeService
    {
        private readonly IRepository<ResourceType> _resourceTypeRepository;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public ResourceTypeService(IRepository<ResourceType> resourceTypeRepository, IMapper mapper)
        {
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task AddResourceType(ResourceTypeDto resourceTypeDto)
        {
            await Validate(resourceTypeDto);
            var mappedResourceType = _mapper.Map<ResourceType>(resourceTypeDto);
            await _resourceTypeRepository.Add(mappedResourceType);
            await _resourceTypeRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteResourceTypeById(int id)
        {
            await ResourceTypeIsExists(id);
            await _resourceTypeRepository.Delete(id);
            await _resourceTypeRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceTypeDto>> GetAllResourceTypes()
        {
            var resourceTypes = _resourceTypeRepository.GetAll().AsEnumerable();
            return _mapper.Map<IEnumerable<ResourceTypeDto>>(resourceTypes);
        }

        /// <inheritdoc />
        public async Task<ResourceTypeDto> GetResourceType(Func<ResourceTypeDto, bool> predicate)
        {
            return (await GetAllResourceTypes()).FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public async Task<ResourceTypeDto> GetResourceTypeById(int id)
        {
            var resourceType = await _resourceTypeRepository.GetById(id);
            return _mapper.Map<ResourceTypeDto>(resourceType);
        }

        /// <inheritdoc />
        public async Task UpdateResourceType(ResourceTypeDto resourceTypeDto)
        {
            await Validate(resourceTypeDto);
            var resourceType = _mapper.Map<ResourceType>(resourceTypeDto);
            await _resourceTypeRepository.Update(resourceType);
            await _resourceTypeRepository.Save();
        }

        private async Task Validate(ResourceTypeDto resourceType)
        {
            if (string.IsNullOrEmpty(resourceType.Name))
            {
                throw new ArgumentException("Название типа ресурса не может быть пустым.");
            }

            if (string.IsNullOrEmpty(resourceType.Description))
            {
                throw new ArgumentException("Описание типа ресурса не может быть пустым.");
            }
        }

        private async Task ResourceTypeIsExists(int id)
        {
            var resourceTypeById = await _resourceTypeRepository.GetById(id);
            if (resourceTypeById == null)
            {
                throw new ArgumentNullException(nameof(resourceTypeById), "Типа ресурса с таким ИД не существует.");
            }
        }
    }
}

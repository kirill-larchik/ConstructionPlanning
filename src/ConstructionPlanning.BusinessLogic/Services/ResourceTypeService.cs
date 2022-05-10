using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ResourceTypeService : IResourceTypeService
    {
        private readonly IRepository<ResourceType> _resourceTypeRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<ResourceType, ResourceTypeDto> _paginationService;

        /// <inheritdoc />
        public ResourceTypeService(IRepository<ResourceType> resourceTypeRepository,
            IMapper mapper,
            IPaginationService<ResourceType, ResourceTypeDto> paginationService)
        {
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
            _paginationService = paginationService;
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
            await GetResourceTypeById(id);
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
        public async Task<IEnumerable<ResourceTypeDto>> GetAllResourceTypesByPagination(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null);
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _resourceTypeRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task<ResourceTypeDto> GetResourceTypeById(int id)
        {
            var resourceTypeById = await _resourceTypeRepository.GetById(id);
            if (resourceTypeById == null)
            {
                throw new ArgumentNullException(nameof(resourceTypeById), "Типа ресурса с таким ИД не существует.");
            }

            return _mapper.Map<ResourceTypeDto>(resourceTypeById);
        }

        /// <inheritdoc />
        public async Task UpdateResourceType(ResourceTypeDto resourceTypeDto)
        {
            await GetResourceTypeById(resourceTypeDto.Id);
            await Validate(resourceTypeDto, true);

            var resourceType = _mapper.Map<ResourceType>(resourceTypeDto);
            await _resourceTypeRepository.Update(resourceType);
            await _resourceTypeRepository.Save();
        }

        private async Task Validate(ResourceTypeDto resourceType, bool isUpdate = false)
        {
            if (string.IsNullOrEmpty(resourceType.Name))
            {
                throw new ArgumentException("Название типа ресурса не может быть пустым.");
            }

            await ValidateNameUnique(resourceType, isUpdate);
            if (string.IsNullOrEmpty(resourceType.Description))
            {
                throw new ArgumentException("Описание типа ресурса не может быть пустым.");
            }
        }

        private async Task ValidateNameUnique(ResourceTypeDto resourceTypeDto, bool isUpdate)
        {
            var resourceTypes = _resourceTypeRepository.GetAll();
            var resourceTypeName = isUpdate ? (await _resourceTypeRepository.GetById(resourceTypeDto.Id)).Name : string.Empty;
            if ((!isUpdate && resourceTypes.Any(x => x.Name == resourceTypeDto.Name)) ||
                (isUpdate && resourceTypes.Where(x => x.Name != resourceTypeName).Any(x => x.Name == resourceTypeDto.Name)))
            {
                throw new ArgumentException("Тип ресурса с таким названием уже существует.");
            }
        }
    }
}

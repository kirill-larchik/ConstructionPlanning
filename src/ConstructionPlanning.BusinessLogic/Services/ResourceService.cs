using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ResourceService : IResourceService
    {
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<ResourceType> _typeRepository;
        private readonly IPaginationService<Resource, ResourceDto> _paginationService;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public ResourceService(IRepository<Resource> resourceRepository,
            IRepository<ResourceType> typeRepository,
            IMapper mapper,
            IPaginationService<Resource, ResourceDto> paginationService)
        {
            _resourceRepository = resourceRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddResource(ResourceDto resourceDto)
        {
            await Validate(resourceDto);
            var mappedResource = _mapper.Map<Resource>(resourceDto);
            await _resourceRepository.Add(mappedResource);
            await _resourceRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteResourceById(int id)
        {
            await IsResourceExists(id);
            await _resourceRepository.Delete(id);
            await _resourceRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceDto>> GetAllResources()
        {
            var resources = _resourceRepository.GetAll(x => x.Sales,
                x => x.Deliveries,
                x => x.Type)
                .AsEnumerable();

            return _mapper.Map<IEnumerable<ResourceDto>>(resources);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceDto>> GetAllResourcesByPagination(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null, x => x.Type);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceDto>> GetAllResourcesByResourceTypeIdWithPagination(int resourceTypeId, int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, x => x.TypeId == resourceTypeId, x => x.Type);
        }

        /// <inheritdoc />
        public async Task<ResourceDto> GetResource(Func<ResourceDto, bool> predicate)
        {
            return (await GetAllResources()).FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public async Task<ResourceDto> GetResourceById(int id)
        {
            await IsResourceExists(id);
            var resource = await GetResource(x => x.Id == id);
            return _mapper.Map<ResourceDto>(resource);
        }

        /// <inheritdoc />
        public async Task UpdateResource(ResourceDto resourceDto)
        {
            await IsResourceExists(resourceDto.Id);
            await Validate(resourceDto, true);
            var resource = _mapper.Map<Resource>(resourceDto);
            await _resourceRepository.Update(resource);
            await _resourceRepository.Save();
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        { 
            return await _resourceRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCountByResourceTypeId(int resourceTypeId)
        {
            return await _resourceRepository.GetAll().Where(x => x.TypeId == resourceTypeId).CountAsync();
        }

        private async Task Validate(ResourceDto resource, bool isUpdate = false)
        {
            if (await _typeRepository.GetById(resource.TypeId) == null)
            {
                throw new ArgumentException("Тип ресурса с таким ИД не существует.");
            }

            if (string.IsNullOrEmpty(resource.Name))
            {
                throw new ArgumentException("Название ресурса не может быть пустым.");
            }

            await ValidateNameUnique(resource, isUpdate);
            if (resource.AvaliableAmount <= 0)
            {
                throw new ArgumentException("Доступное количество ресурсов на складе не может быть меньше или равно нулю.");
            }

            if (resource.UnitCost <= 0)
            {
                throw new ArgumentException("Цена зе еденицу ресурса должна быть больше нуля.");
            }
        }

        private async Task ValidateNameUnique(ResourceDto resourceDto, bool isUpdate)
        {
            var resources = _resourceRepository.GetAll();
            var resourceName = isUpdate ? (await _resourceRepository.GetById(resourceDto.Id)).Name : string.Empty;
            if ((!isUpdate && resources.Any(x => x.Name == resourceDto.Name)) ||
                (isUpdate && resources.Where(x => x.Name != resourceName).Any(x => x.Name == resourceDto.Name)))
            {
                throw new ArgumentException("Ресурс с таким названием уже существует.");
            }
        }

        private async Task IsResourceExists(int id)
        {
            var resourceById = await _resourceRepository.GetById(id);
            if (resourceById == null)
            {
                throw new ArgumentNullException(nameof(resourceById), "Ресурса с таким ИД не существует.");
            }
        }
    }
}

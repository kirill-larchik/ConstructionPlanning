using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ResourcePerObjectService : IResourcePerObjectService
    {
        private readonly IRepository<ResourcePerObject> _resourcePerObjectRepository;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<ConstructionObject> _constructionObjectRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<ResourcePerObject, ResourcePerObjectDto> _paginationService;

        /// <inheritdoc />
        public ResourcePerObjectService(IRepository<ResourcePerObject> resourceTypeRepository,
            IRepository<Resource> resourceRepository,
            IRepository<ConstructionObject> constructionObjectRepository,
            IMapper mapper,
            IPaginationService<ResourcePerObject, ResourcePerObjectDto> paginationService)
        {
            _resourcePerObjectRepository = resourceTypeRepository;
            _resourceRepository = resourceRepository;
            _constructionObjectRepository = constructionObjectRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddResourcePerObject(ResourcePerObjectDto resourcePerObjectDto)
        {
            await Validate(resourcePerObjectDto);
            var mappedResourcePerObject = _mapper.Map<ResourcePerObject>(resourcePerObjectDto);
            await _resourcePerObjectRepository.Add(mappedResourcePerObject);
            await _resourcePerObjectRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteResourcePerObjectById(int id)
        {
            await GetResourcePerObjectById(id);
            await _resourcePerObjectRepository.Delete(id);
            await _resourcePerObjectRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourcePerObjectDto>> GetAllResourcePerObjects()
        {
            var resourcePerObjects = _resourcePerObjectRepository.GetAll().AsEnumerable();
            return _mapper.Map<IEnumerable<ResourcePerObjectDto>>(resourcePerObjects);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourcePerObjectDto>> GetAllPaginatedResourcePerObjects(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null);
        }

        /// <inheritdoc />
        public async Task<ResourcePerObjectDto> GetResourcePerObjectById(int id)
        {
            var resourcePerObjectById = await _resourcePerObjectRepository.GetById(id);
            if (resourcePerObjectById == null)
            {
                throw new ArgumentNullException(nameof(resourcePerObjectById), "Поставщика с таким ИД не существует.");
            }

            return _mapper.Map<ResourcePerObjectDto>(resourcePerObjectById);
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _resourcePerObjectRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateResourcePerObject(ResourcePerObjectDto resourcePerObjectDto)
        {
            await GetResourcePerObjectById(resourcePerObjectDto.Id);
            await Validate(resourcePerObjectDto, true);

            var resourcePerObject = _mapper.Map<ResourcePerObject>(resourcePerObjectDto);
            await _resourcePerObjectRepository.Update(resourcePerObject);
            await _resourcePerObjectRepository.Save();
        }

        /// <inheritdoc />
        private async Task Validate(ResourcePerObjectDto resourcePerObjectDto, bool isUpdate = false)
        {
            if (await _resourceRepository.GetById(resourcePerObjectDto.ResourceId) == null)
            {
                throw new ArgumentException("Ресурса с таким ИД не существует.");
            }

            if (await _constructionObjectRepository.GetById(resourcePerObjectDto.ResourceId) == null)
            {
                throw new ArgumentException("Строительного объекта с таким ИД не существует.");
            }

            if (resourcePerObjectDto.Count <= 0)
            {
                throw new ArgumentException("Количество ресурсов на объект должно быть больше нуля.");
            }
        }
    }
}

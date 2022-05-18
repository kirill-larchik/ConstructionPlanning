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
        public ResourcePerObjectService(IRepository<ResourcePerObject> resourcePerObjectRepository,
            IRepository<Resource> resourceRepository,
            IRepository<ConstructionObject> constructionObjectRepository,
            IMapper mapper,
            IPaginationService<ResourcePerObject, ResourcePerObjectDto> paginationService)
        {
            _resourcePerObjectRepository = resourcePerObjectRepository;
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

            await UpdateAvaliableAmountForResource(resourcePerObjectDto);
        }

        /// <inheritdoc />
        public async Task DeleteResourcePerObjectById(int id)
        {
            var resourcePerObject = await GetResourcePerObjectById(id);
            await ReturnUsedCountToResource(resourcePerObject);
            await _resourcePerObjectRepository.Delete(id);
            await _resourcePerObjectRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourcePerObjectDto>> GetAllResourcePerObjects()
        {
            var resourcePerObjects = _resourcePerObjectRepository.GetAll(x => x.Resource, x => x.ConstructionObject).AsEnumerable();
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
            var resourcePerObjectById = await _resourcePerObjectRepository.GetById(id, x => x.Resource, x => x.ConstructionObject);
            if (resourcePerObjectById == null)
            {
                throw new ArgumentNullException(nameof(resourcePerObjectById), "Ресусра для объекта с таким ИД не существует.");
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
            await UpdateAvaliableAmountForResource(resourcePerObjectDto, true);
            await _resourcePerObjectRepository.Update(resourcePerObject);
            await _resourcePerObjectRepository.Save();
        }

        /// <inheritdoc />
        private async Task Validate(ResourcePerObjectDto resourcePerObjectDto, bool isUpdate = false)
        {
            var resoruce = await _resourceRepository.GetById(resourcePerObjectDto.ResourceId);
            if (resoruce == null)
            {
                throw new ArgumentException("Ресурса с таким ИД не существует.");
            }

            await ValidateUniqueResource(resourcePerObjectDto, isUpdate);
            if (await _constructionObjectRepository.GetById(resourcePerObjectDto.ConstructionObjectId) == null)
            {
                throw new ArgumentException("Строительного объекта с таким ИД не существует.");
            }

            if (resourcePerObjectDto.Count <= 0)
            {
                throw new ArgumentException("Количество ресурсов на объект должно быть больше нуля.");
            }

            await ValidateAvaliableAmount(resourcePerObjectDto, isUpdate, resoruce);
        }

        private async Task ValidateUniqueResource(ResourcePerObjectDto resourcePerObjectDto, bool isUpdate)
        {
            var resourcesPerObject = _resourcePerObjectRepository.GetAll().Where(x => x.ConstructionObjectId == resourcePerObjectDto.ConstructionObjectId);
            var resourceId = isUpdate ? (await _resourcePerObjectRepository.GetById(resourcePerObjectDto.Id)).ResourceId : -1;
            if ((!isUpdate && resourcesPerObject.Any(x => x.ResourceId == resourcePerObjectDto.ResourceId)) ||
                (isUpdate && resourcesPerObject.Where(x => x.ResourceId != resourceId).Any(x => x.ResourceId == resourcePerObjectDto.ResourceId)))
            {
                throw new ArgumentException("Ресурс для строительного объекта уже существует.");
            }
        }

        private async Task ValidateAvaliableAmount(ResourcePerObjectDto resourcePerObjectDto, bool isUpdate, Resource resource)
        {
            if (isUpdate)
            {
                int avaliableAmount = await GetAvaliableAmountByUpdateSale(resourcePerObjectDto, resource);
                if (avaliableAmount < 0)
                {
                    throw new ArgumentException($"Количество продаваемых ресурсов не должно превышать количество на складе ({Math.Abs(avaliableAmount)} ресурса(ов) не хватает).");
                }
            }
            else if (resourcePerObjectDto.Count > resource.AvaliableAmount)
            {
                throw new ArgumentException($"Количество продаваемых ресурсов не должно превышать количество на складе ({resource.AvaliableAmount}).");
            }
        }

        private async Task<int> GetAvaliableAmountByUpdateSale(ResourcePerObjectDto resourcePerObjectDto, Resource resource)
        {
            var resourcePerObject = await _resourcePerObjectRepository.GetById(resourcePerObjectDto.Id);
            var countOffset = resourcePerObjectDto.Count - resourcePerObject.Count;
            var avaliableAmount = resource.AvaliableAmount - countOffset;
            return avaliableAmount;
        }

        private async Task UpdateAvaliableAmountForResource(ResourcePerObjectDto resourcePerObjectDto, bool isUpdate = false)
        {
            var resource = await _resourceRepository.GetById(resourcePerObjectDto.ResourceId);
            if (isUpdate)
            {
                var resourcePerObject = await _resourcePerObjectRepository.GetById(resourcePerObjectDto.Id);
                var offset = resourcePerObjectDto.Count - resourcePerObject.Count;
                resource.AvaliableAmount -= offset;
                if (resource.AvaliableAmount < 0)
                {
                    throw new ArgumentException($"Количество ресурсов на складе не должно быть меньше нуля ({Math.Abs(resource.AvaliableAmount)}) требуется.");
                }
            }
            else
            {
                resource.AvaliableAmount -= resourcePerObjectDto.Count;
            }

            await _resourceRepository.Update(resource);
            await _resourceRepository.Save();
        }

        private async Task ReturnUsedCountToResource(ResourcePerObjectDto resourcePerObject)
        {
            var resource = await _resourceRepository.GetById(resourcePerObject.ResourceId);
            resource.AvaliableAmount += resourcePerObject.Count;
            await _resourceRepository.Update(resource);
            await _resourceRepository.Save();
            _resourceRepository.ClearTracker();
        }

    }
}

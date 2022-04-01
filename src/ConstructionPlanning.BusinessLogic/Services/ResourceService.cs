using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ResourceService : IResourceService
    {
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<ResourceType> _typeRepository;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public ResourceService(IRepository<Resource> resourceRepository,
            IRepository<ResourceType> typeRepository,
            IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
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
            await ResourceIsExists(id);
            await _resourceRepository.Delete(id);
            await _resourceRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceDto>> GetAllResources()
        {
            var resources = _resourceRepository.GetAll(x => x.Sales,
                x => x.Deliveries,
                x => x.Type,
                x => x.ResourcesPerObject)
                .AsEnumerable();

            return _mapper.Map<IEnumerable<ResourceDto>>(resources);
        }

        /// <inheritdoc />
        public async Task<ResourceDto> GetResource(Func<ResourceDto, bool> predicate)
        {
            return (await GetAllResources()).FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public async Task<ResourceDto> GetResourceById(int id)
        {
            var resource = await _resourceRepository.GetById(id);
            return _mapper.Map<ResourceDto>(resource);
        }

        /// <inheritdoc />
        public async Task UpdateResource(ResourceDto resourceDto)
        {
            await Validate(resourceDto);
            var resource = _mapper.Map<Resource>(resourceDto);
            await _resourceRepository.Update(resource);
            await _resourceRepository.Save();
        }

        private async Task Validate(ResourceDto resource)
        {
            if (await _typeRepository.GetById(resource.TypeId) == null)
            {
                throw new ArgumentException("Тип ресурса с таким ИД не существует.");
            }

            if (string.IsNullOrEmpty(resource.Name))
            {
                throw new ArgumentException("Название ресурса не может быть пустым.");
            }

            if (resource.UnitCost <= 0)
            {
                throw new ArgumentException("Цена зе еденицу ресурса должна быть больше нуля.");
            }

            if (resource.AvaliableAmount <= 0)
            {
                throw new ArgumentException("Доступное количество ресурсов на складе не может быть меньше или равно нулю.");
            }
        }

        private async Task ResourceIsExists(int id)
        {
            var resourceById = await _resourceRepository.GetById(id);
            if (resourceById == null)
            {
                throw new ArgumentNullException(nameof(resourceById), "Ресурса с таким ИД не существует.");
            }
        }
    }
}

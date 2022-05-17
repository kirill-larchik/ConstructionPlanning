using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ConstructionObjectService : IConstructionObjectService
    {
        private readonly IRepository<ConstructionObject> _constructionObjectRepository;
        private readonly IRepository<ResourcePerObject> _resourcePerObjectRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<ConstructionObject, ConstructionObjectDto> _paginationService;

        /// <inheritdoc />
        public ConstructionObjectService(IRepository<ConstructionObject> constructionObjectRepository,
            IRepository<ResourcePerObject> resourcePerObjectRepository,
            IRepository<Project> projectRepository,
            IMapper mapper,
            IPaginationService<ConstructionObject, ConstructionObjectDto> paginationService)
        {
            _constructionObjectRepository = constructionObjectRepository;
            _resourcePerObjectRepository = resourcePerObjectRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _paginationService = paginationService;
            _resourcePerObjectRepository = resourcePerObjectRepository;
        }

        /// <inheritdoc />
        public async Task AddConstructionObject(ConstructionObjectDto constructionObjectDto)
        {
            await Validate(constructionObjectDto);
            var mappedConstructionObject = _mapper.Map<ConstructionObject>(constructionObjectDto);
            await _constructionObjectRepository.Add(mappedConstructionObject);
            await _constructionObjectRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteConstructionObjectById(int id)
        {
            await GetConstructionObjectById(id);
            await _constructionObjectRepository.Delete(id);
            await _constructionObjectRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ConstructionObjectDto>> GetAllConstructionObjects()
        {
            var constructionObjects = _constructionObjectRepository.GetAll(x => x.Project, x => x.ResourcesPerObject).AsEnumerable();
            var mappedConstructionObjects = _mapper.Map<IEnumerable<ConstructionObjectDto>>(constructionObjects);
            FillTotalCost(mappedConstructionObjects);

            return mappedConstructionObjects;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ConstructionObjectDto>> GetAllPaginatedConstructionObjects(int page, int pageSize)
        {
            var constructionObjects =  await _paginationService.GetItems(page, pageSize, null);
            FillTotalCost(constructionObjects);

            return constructionObjects;
        }

        /// <inheritdoc />
        public async Task<ConstructionObjectDto> GetConstructionObjectById(int id)
        {
            var constructionObjectById = await _constructionObjectRepository.GetById(id, x => x.Project, x => x.ResourcesPerObject);
            if (constructionObjectById == null)
            {
                throw new ArgumentNullException(nameof(constructionObjectById), "Строительного объекта с таким ИД не существует.");
            }

            var constructionObject = _mapper.Map<ConstructionObjectDto>(constructionObjectById);
            FillTotalCost(new List<ConstructionObjectDto> { constructionObject });

            return constructionObject;
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _constructionObjectRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateConstructionObject(ConstructionObjectDto constructionObjectDto)
        {
            await GetConstructionObjectById(constructionObjectDto.Id);
            await Validate(constructionObjectDto, true);

            var constructionObject = _mapper.Map<ConstructionObject>(constructionObjectDto);
            await _constructionObjectRepository.Update(constructionObject);
            await _constructionObjectRepository.Save();
        }

        /// <inheritdoc />
        private async Task Validate(ConstructionObjectDto constructionObjectDto, bool isUpdate = false)
        {
            if (await _projectRepository.GetById(constructionObjectDto.ProjectId) == null)
            {
                throw new ArgumentException("Проекта с таким ИД не существует.");
            }

            if (string.IsNullOrEmpty(constructionObjectDto.Name))
            {
                throw new ArgumentException("Название объекта не может быть пустым.");
            }

            await ValidateNameUnique(constructionObjectDto, isUpdate);
            if (string.IsNullOrEmpty(constructionObjectDto.Description))
            {
                throw new ArgumentException("Описание объекта не может быть пустым.");
            }
        }

        private async Task ValidateNameUnique(ConstructionObjectDto constructionObjectDto, bool isUpdate)
        {
            var constructionObjects = _constructionObjectRepository.GetAll().Where(x => x.ProjectId == constructionObjectDto.ProjectId);
            var constractionObjectName = isUpdate ? (await _constructionObjectRepository.GetById(constructionObjectDto.Id)).Name : string.Empty;
            if ((!isUpdate && constructionObjects.Any(x => x.Name == constructionObjectDto.Name)) ||
                (isUpdate && constructionObjects.Where(x => x.Name != constractionObjectName).Any(x => x.Name == constructionObjectDto.Name)))
            {
                throw new ArgumentException("Строительный объект с таким названием уже существует для проекта.");
            }
        }

        private async Task FillTotalCost(IEnumerable<ConstructionObjectDto> mappedConstructionObjects)
        {
            foreach (var constructionObject in mappedConstructionObjects)
            {
                var resourcesPerObjectById = _resourcePerObjectRepository.GetAll(x => x.Resource).Where(x => x.ConstructionObjectId == constructionObject.Id);
                constructionObject.ResourcesPerObject = _mapper.Map<IEnumerable<ResourcePerObjectDto>>(resourcesPerObjectById.AsEnumerable());
                constructionObject.TotalCost = constructionObject.ResourcesPerObject?.Sum(x => x.TotalCost) ?? 0;
            }
        }
    }
}

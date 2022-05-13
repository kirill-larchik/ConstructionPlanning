using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<Project, ProjectDto> _paginationService;

        /// <inheritdoc />
        public ProjectService(IRepository<Project> projectRepository,
            IRepository<Customer> customerRepository,
            IMapper mapper,
            IPaginationService<Project, ProjectDto> paginationService)
        {
            _projectRepository = projectRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddProject(ProjectDto projectDto)
        {
            await Validate(projectDto);
            var mappedProject = _mapper.Map<Project>(projectDto);
            await _projectRepository.Add(mappedProject);
            await _projectRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteProjectById(int id)
        {
            await GetProjectById(id);
            await _projectRepository.Delete(id);
            await _projectRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProjectDto>> GetAllProjects()
        {
            var projects = _projectRepository.GetAll().AsEnumerable();
            var mappedProjects = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            FillTotalCost(mappedProjects);

            return mappedProjects;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProjectDto>> GetAllPaginatedProjects(int page, int pageSize)
        {
            var projects = await _paginationService.GetItems(page, pageSize, null);
            FillTotalCost(projects);

            return projects;
        }

        /// <inheritdoc />
        public async Task<ProjectDto> GetProjectById(int id)
        {
            var projectById = await _projectRepository.GetById(id);
            if (projectById == null)
            {
                throw new ArgumentNullException(nameof(projectById), "Проекта с таким ИД не существует.");
            }

            var project = _mapper.Map<ProjectDto>(projectById);
            FillTotalCost(new List<ProjectDto> { project });

            return project;
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _projectRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateProject(ProjectDto projectDto)
        {
            await GetProjectById(projectDto.Id);
            await Validate(projectDto, true);

            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.Update(project);
            await _projectRepository.Save();
        }

        /// <inheritdoc />
        private async Task Validate(ProjectDto projectDto, bool isUpdate = false)
        {
            if (await _customerRepository.GetById(projectDto.CustomerId) == null)
            {
                throw new ArgumentException("Заказчика с таким ИД не существует.");
            }

            if (string.IsNullOrEmpty(projectDto.Name))
            {
                throw new ArgumentException("Название поставщика не может быть пустым.");
            }

            await ValidateNameUnique(projectDto, isUpdate);
            var now = DateTime.UtcNow.Date;
            if (projectDto.Deadline.ToUniversalTime() <= now)
            {
                throw new ArgumentException("Неверная дата крайнего срока проекта.");
            }

            if (projectDto.AllocatedAmount <= 0)
            {
                throw new ArgumentException("Количество выделенных средств должно быть больше нуля.");
            }
        }

        private async Task ValidateNameUnique(ProjectDto projectDto, bool isUpdate)
        {
            var projects = _projectRepository.GetAll();
            var constractionObjectName = isUpdate ? (await _projectRepository.GetById(projectDto.Id)).Name : string.Empty;
            if ((!isUpdate && projects.Any(x => x.Name == projectDto.Name)) ||
                (isUpdate && projects.Where(x => x.Name != constractionObjectName).Any(x => x.Name == projectDto.Name)))
            {
                throw new ArgumentException("Проект с таким названием уже существует для проекта.");
            }
        }

        private static void FillTotalCost(IEnumerable<ProjectDto> mappedProjects)
        {
            foreach (var project in mappedProjects)
            {
                project.TotalCost = project.ConstructionObjects?.Sum(x => x.TotalCost) ?? 0;
            }
        }
    }
}

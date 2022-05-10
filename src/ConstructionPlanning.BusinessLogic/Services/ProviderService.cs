using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class ProviderService : IProviderService
    {
        private readonly IRepository<Provider> _providerRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<Provider, ProviderDto> _paginationService;

        /// <inheritdoc />
        public ProviderService(IRepository<Provider> resourceTypeRepository,
            IMapper mapper,
            IPaginationService<Provider, ProviderDto> paginationService)
        {
            _providerRepository = resourceTypeRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddProvider(ProviderDto providerDto)
        {
            await Validate(providerDto);
            var mappedProvider = _mapper.Map<Provider>(providerDto);
            await _providerRepository.Add(mappedProvider);
            await _providerRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteProviderById(int id)
        {
            await GetProviderById(id);
            await _providerRepository.Delete(id);
            await _providerRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProviderDto>> GetAllProviders()
        {
            var providers = _providerRepository.GetAll().AsEnumerable();
            return _mapper.Map<IEnumerable<ProviderDto>>(providers);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProviderDto>> GetAllPaginatedProviders(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null);
        }

        /// <inheritdoc />
        public async Task<ProviderDto> GetProviderById(int id)
        {
            var providerById = await _providerRepository.GetById(id);
            if (providerById == null)
            {
                throw new ArgumentNullException(nameof(providerById), "Поставщика с таким ИД не существует.");
            }

            return _mapper.Map<ProviderDto>(providerById);
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _providerRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateProvider(ProviderDto providerDto)
        {
            await GetProviderById(providerDto.Id);
            await Validate(providerDto, true);

            var provider = _mapper.Map<Provider>(providerDto);
            await _providerRepository.Update(provider);
            await _providerRepository.Save();
        }

        /// <inheritdoc />
        private async Task Validate(ProviderDto providerDto, bool isUpdate = false)
        {
            if (string.IsNullOrEmpty(providerDto.Name))
            {
                throw new ArgumentException("Название поставщика не может быть пустым.");
            }

            await ValidateNameUnique(providerDto, isUpdate);
            if (string.IsNullOrEmpty(providerDto.Address))
            {
                throw new ArgumentException("Адрес поставщика не может быть пустым.");
            }

            var regex = new Regex("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$");
            if (string.IsNullOrEmpty(providerDto.Phone) && regex.IsMatch(providerDto.Phone))
            {
                throw new ArgumentException("Неверный телефон поставщика.");
            }
        }

        /// <inheritdoc />
        private async Task ValidateNameUnique(ProviderDto providerDto, bool isUpdate)
        {
            var providers = _providerRepository.GetAll();
            var providerName = isUpdate ? (await _providerRepository.GetById(providerDto.Id)).Name : string.Empty;
            if ((!isUpdate && providers.Any(x => x.Name == providerDto.Name)) ||
                (isUpdate && providers.Where(x => x.Name != providerName).Any(x => x.Name == providerDto.Name)))
            {
                throw new ArgumentException("Поставщик с таким названием уже существует.");
            }
        }
    }
}

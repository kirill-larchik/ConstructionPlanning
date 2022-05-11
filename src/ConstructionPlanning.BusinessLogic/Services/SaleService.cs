using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class SaleService : ISaleService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IPaginationService<Sale, SaleDto> _paginationService;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public SaleService(IRepository<Sale> saleRepository,
            IRepository<Resource> resourceRepository,
            IMapper mapper,
            IPaginationService<Sale, SaleDto> paginationService)
        {
            _saleRepository = saleRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddSale(SaleDto saleDto)
        {
            await Validate(saleDto);

            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.Add(sale);
            await _saleRepository.Save();

            await UpdateAvaliableAmountForResource(saleDto);
        }

        /// <inheritdoc />
        public async Task DeleteSaleById(int id)
        {
            await GetSaleById(id);

            await _saleRepository.Delete(id);
            await _saleRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SaleDto>> GetAllSales()
        {
            var sales = _saleRepository.GetAll(x => x.Resource).AsEnumerable();

            return _mapper.Map<IEnumerable<SaleDto>>(sales);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SaleDto>> GetAllPaginatedSales(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SaleDto>> GetAllPaginatedSalesByResourceId(int resourceId, int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, x => x.ResourceId == resourceId, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<SaleDto> GetSaleById(int id)
        {
            var saleById = await _saleRepository.GetById(id, x => x.Resource);
            if (saleById == null)
            {
                throw new ArgumentNullException(nameof(saleById), "Продажи с таким номером не существует.");
            }

            return _mapper.Map<SaleDto>(saleById);
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _saleRepository.GetAll().CountAsync();
        }

        public async Task<int> GetTotalCountByResourceId(int resourceId)
        {
            return await _saleRepository.GetAll().Where(x => x.ResourceId == resourceId).CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateSale(SaleDto saleDto)
        {
            await GetSaleById(saleDto.Id);
            await Validate(saleDto, true);

            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.Update(sale);
            await _saleRepository.Save();

            await UpdateAvaliableAmountForResource(saleDto, true);
        }

        private async Task Validate(SaleDto saleDto, bool isUpdate = false)
        {
            var resource = await _resourceRepository.GetById(saleDto.ResourceId);
            if (resource == null)
            {
                throw new ArgumentException("Ресурса с таким ИД не существует.");
            }

            if (saleDto.Count <= 0)
            {
                throw new ArgumentException("Количество ресурсов должно быть больше нуля.");
            }

            await ValidateAvaliableAmount(saleDto, isUpdate, resource);

            if (saleDto.Date == default || (!isUpdate && saleDto.Date.ToUniversalTime() <= DateTime.UtcNow))
            {
                throw new ArgumentException("Неверная дата продажи.");
            }

            if (string.IsNullOrEmpty(saleDto.Customer))
            {
                throw new ArgumentException("Имя заказчика не может быть пустым.");
            }
        }

        private async Task ValidateAvaliableAmount(SaleDto saleDto, bool isUpdate, Resource resource)
        {
            if (isUpdate)
            {
                int avaliableAmount = await GetAvaliableAmountByUpdateSale(saleDto, resource);
                if (avaliableAmount < 0)
                {
                    throw new ArgumentException($"Количество продаваемых ресурсов не должно превышать количество на складе ({Math.Abs(avaliableAmount)} ресурса(ов) не хватает).");
                }
            }
            else if (saleDto.Count > resource.AvaliableAmount)
            {
                throw new ArgumentException($"Количество продаваемых ресурсов не должно превышать количество на складе ({resource.AvaliableAmount}).");
            }
        }

        private async Task<int> GetAvaliableAmountByUpdateSale(SaleDto saleDto, Resource resource)
        {
            var sale = await _saleRepository.GetById(saleDto.Id);
            var countOffset = saleDto.Count - sale.Count;
            var avaliableAmount = resource.AvaliableAmount - countOffset;
            return avaliableAmount;
        }

        private async Task UpdateAvaliableAmountForResource(SaleDto saleDto, bool isUpdate = false)
        {
            var resource = await _resourceRepository.GetById(saleDto.ResourceId);
            if (isUpdate)
            {
                var sale = await _saleRepository.GetById(saleDto.Id);
                var offset = saleDto.Count - sale.Count;
                resource.AvaliableAmount -= offset;
                if (resource.AvaliableAmount < 0)
                {
                    throw new ArgumentException($"Количество ресурсов на складе не должно быть меньше нуля ({Math.Abs(resource.AvaliableAmount)}) требуется.");
                }
            }
            else
            {
                resource.AvaliableAmount -= saleDto.Count;
            }

            await _resourceRepository.Update(resource);
            await _resourceRepository.Save();
        }
    }
}

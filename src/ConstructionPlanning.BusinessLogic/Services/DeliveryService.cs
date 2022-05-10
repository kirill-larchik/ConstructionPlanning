using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConstructionPlanning.BusinessLogic.Services
{
    /// <inheritdoc />
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<Delivery> _deliveryRepository;
        private readonly IRepository<Provider> _providerRepository;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IPaginationService<Delivery, DeliveryDto> _paginationService;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public DeliveryService(IRepository<Delivery> deliveryRepository,
            IRepository<Provider> providerRepository,
            IRepository<Resource> resourceRepository,
            IMapper mapper,
            IPaginationService<Delivery, DeliveryDto> paginationService)
        {
            _deliveryRepository = deliveryRepository;
            _providerRepository = providerRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddDelivery(DeliveryDto deliveryDto)
        {
            await Validate(deliveryDto);

            var delivery = _mapper.Map<Delivery>(deliveryDto);
            await _deliveryRepository.Add(delivery);
            await _deliveryRepository.Save();

            await UpdateResourceAvaliableAmount(deliveryDto);
        }

        /// <inheritdoc />
        public async Task DeleteDeliveryById(int id)
        {
            await GetDeliveryById(id);
            await _deliveryRepository.Delete(id);
            await _deliveryRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeliveryDto>> GetAllDeliveries()
        {
            var deliveries = _deliveryRepository.GetAll(x => x.Provider, x => x.Resource).AsEnumerable();
            return _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeliveryDto>> GetAllPaginatedDeliveries(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null,
                x => x.Provider, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeliveryDto>> GetAllPaginatedDeliveriesByProviderId(int providerId, int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, x => x.ProviderId == providerId,
                x => x.Provider, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeliveryDto>> GetAllPaginatedDeliveriesByResourceId(int resourceId, int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, x => x.ResourceId == resourceId,
                x => x.Provider, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<DeliveryDto> GetDeliveryById(int id)
        {
            var deliveryById = await _deliveryRepository.GetById(id);
            if (deliveryById == null)
            {
                throw new ArgumentNullException(nameof(deliveryById), "Поставки с таким ИД не существует.");
            }

            return _mapper.Map<DeliveryDto>(deliveryById);
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _deliveryRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCountByProviderId(int providerId)
        {
            return await _deliveryRepository.GetAll().Where(x => x.ProviderId == providerId).CountAsync();
        }

        public async Task<int> GetTotalCountByResourceId(int resourceId)
        {
            return await _deliveryRepository.GetAll().Where(x => x.ResourceId == resourceId).CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateDelivery(DeliveryDto deliveryDto)
        {
            await GetDeliveryById(deliveryDto.Id);
            await Validate(deliveryDto);

            var delivery = _mapper.Map<Delivery>(deliveryDto);
            await _deliveryRepository.Update(delivery);
            await _deliveryRepository.Save();

            await UpdateResourceAvaliableAmount(deliveryDto, true);
        }

        private async Task Validate(DeliveryDto deliveryDto)
        {
            if (await _providerRepository.GetById(deliveryDto.ProviderId) == null)
            {
                throw new ArgumentException("Поставщика с таким ИД не существует.");
            }

            if (await _resourceRepository.GetById(deliveryDto.ResourceId) == null)
            {
                throw new ArgumentException("Ресурса с таким ИД не существует.");
            }

            if (deliveryDto.Date == default)
            {
                throw new ArgumentException("Неверная дата поставки.");
            }

            if (deliveryDto.UnitCost <= 0)
            {
                throw new ArgumentException("Цена зе еденицу ресурса должна быть больше нуля.");
            }

            if (deliveryDto.Count <= 0)
            {
                throw new ArgumentException("Количество ресурсов должно быть больше нуля.");
            }
        }

        private async Task UpdateResourceAvaliableAmount(DeliveryDto deliveryDto, bool isUpdate = false)
        {
            var resource = await _resourceRepository.GetById(deliveryDto.ResourceId);
            if (isUpdate)
            {
                var delivery = await _deliveryRepository.GetById(deliveryDto.Id);
                var offset = deliveryDto.Count - delivery.Count;
                resource.AvaliableAmount += offset;
                if (resource.AvaliableAmount <= 0)
                {
                    throw new ArgumentException($"Количество ресурсов на складе должно быть больше нуля ({Math.Abs(resource.AvaliableAmount) + 1}) требуется.");
                }
            }
            else
            {
                resource.AvaliableAmount += deliveryDto.Count;
            }

            await _resourceRepository.Update(resource);
            await _resourceRepository.Save();
        }
    }
}

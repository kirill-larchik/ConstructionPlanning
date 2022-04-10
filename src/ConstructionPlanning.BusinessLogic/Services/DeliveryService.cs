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

            delivery.Cost = deliveryDto.UnitCost * deliveryDto.Count;

            await _deliveryRepository.Add(delivery);
            await _deliveryRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteDeliveryById(int id)
        {
            await IsDeliveryExists(id);
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
        public async Task<IEnumerable<DeliveryDto>> GetAllDeliveriesByPagination(int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, null,
                x => x.Provider, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeliveryDto>> GetAllDeliveriesByProviderIdWithPagination(int providerId, int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, x => x.ProviderId == providerId,
                x => x.Provider, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeliveryDto>> GetAllDeliveriesByResourceIdWithPagination(int resourceId, int page, int pageSize)
        {
            return await _paginationService.GetItems(page, pageSize, x => x.ResourceId == resourceId,
                x => x.Provider, x => x.Resource);
        }

        /// <inheritdoc />
        public async Task<DeliveryDto> GetDelivery(Func<DeliveryDto, bool> predicate)
        {
            return (await GetAllDeliveries()).FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public async Task<DeliveryDto> GetDeliveryById(int id)
        {
            await IsDeliveryExists(id);
            var delivery = await GetDelivery(x => x.Id == id);
            return _mapper.Map<DeliveryDto>(delivery);
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
            await IsDeliveryExists(deliveryDto.Id);
            await Validate(deliveryDto);
            var delivery = _mapper.Map<Delivery>(deliveryDto);

            delivery.Cost = delivery.UnitCost * delivery.Count;

            await _deliveryRepository.Update(delivery);
            await _deliveryRepository.Save();
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

        private async Task IsDeliveryExists(int id)
        {
            var deliveryById = await _deliveryRepository.GetById(id);
            if (deliveryById == null)
            {
                throw new ArgumentNullException(nameof(deliveryById), "Поставки с таким ИД не существует.");
            }
        }
    }
}

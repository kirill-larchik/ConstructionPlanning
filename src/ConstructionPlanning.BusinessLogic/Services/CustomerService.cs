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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<Customer, CustomerDto> _paginationService;

        /// <inheritdoc />
        public CustomerService(IRepository<Customer> resourceTypeRepository,
            IMapper mapper,
            IPaginationService<Customer, CustomerDto> paginationService)
        {
            _customerRepository = resourceTypeRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        /// <inheritdoc />
        public async Task AddCustomer(CustomerDto customerDto)
        {
            await Validate(customerDto);
            var mappedCustomer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.Add(mappedCustomer);
            await _customerRepository.Save();
        }

        /// <inheritdoc />
        public async Task DeleteCustomerById(int id)
        {
            await GetCustomerById(id);
            await _customerRepository.Delete(id);
            await _customerRepository.Save();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = _customerRepository.GetAll().AsEnumerable();
            var mappedCustomers = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return mappedCustomers;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CustomerDto>> GetAllPaginatedCustomers(int page, int pageSize)
        {
            var customers = await _paginationService.GetItems(page, pageSize, null);
            return customers;
        }

        /// <inheritdoc />
        public async Task<CustomerDto> GetCustomerById(int id)
        {
            var customerById = await _customerRepository.GetById(id);
            if (customerById == null)
            {
                throw new ArgumentNullException(nameof(customerById), "Заказчика с таким ИД не существует.");
            }

            var customer = _mapper.Map<CustomerDto>(customerById);
            return customer;
        }

        /// <inheritdoc />
        public async Task<int> GetTotalCount()
        {
            return await _customerRepository.GetAll().CountAsync();
        }

        /// <inheritdoc />
        public async Task UpdateCustomer(CustomerDto customerDto)
        {
            await GetCustomerById(customerDto.Id);
            await Validate(customerDto, true);

            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.Update(customer);
            await _customerRepository.Save();
        }

        /// <inheritdoc />
        private async Task Validate(CustomerDto customerDto, bool isUpdate = false)
        {
            if (string.IsNullOrEmpty(customerDto.Name))
            {
                throw new ArgumentException("Имя заказчика не может быть пустым.");
            }

            await ValidateNameUnique(customerDto, isUpdate);
            if (string.IsNullOrEmpty(customerDto.Description))
            {
                throw new ArgumentException("Дополнительная информация о заказчике не может быть пустой.");
            }

            var regex = new Regex("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$");
            if (string.IsNullOrEmpty(customerDto.Phone) || !regex.IsMatch(customerDto.Phone))
            {
                throw new ArgumentException("Неверный телефон заказчика.");
            }
        }

        private async Task ValidateNameUnique(CustomerDto customerDto, bool isUpdate)
        {
            var customers = _customerRepository.GetAll();
            var constractionObjectName = isUpdate ? (await _customerRepository.GetById(customerDto.Id)).Name : string.Empty;
            if ((!isUpdate && customers.Any(x => x.Name == customerDto.Name)) ||
                (isUpdate && customers.Where(x => x.Name != constractionObjectName).Any(x => x.Name == customerDto.Name)))
            {
                throw new ArgumentException("Заказчик с таким именем уже существует.");
            }
        }
    }
}

using AutoMapper;
using ConstructionPlanning.BusinessLogic.DTO;
using ConstructionPlanning.BusinessLogic.Services.Interfaces;
using ConstructionPlanning.DataAccess.Objects;
using ConstructionPlanning.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConstructionPlanning.BusinessLogic.Services.Pagination
{
    /// <inheritdoc />
    public class BasePaginationService<T, TDto> : IPaginationService<T, TDto>
        where T : class, IBaseObject
        where TDto : class, IBaseDtoObject
    {
        private readonly IRepository<T> _repository;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public BasePaginationService(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TDto>> GetItems(int page,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes)
        {
            var items = _repository.GetAll(includes).Where(predicate ?? (x => true));
            var itemsWithPagination = await items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return _mapper.Map<IEnumerable<TDto>>(itemsWithPagination);
        }
    }
}

using System.Linq.Expressions;

namespace ConstructionPlanning.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Сервис пагинации.
    /// </summary>
    public interface IPaginationService<T, TDto>
    {
        /// <summary>
        /// Вовзращает сущности с применением пагинации.
        /// </summary>
        Task<IEnumerable<TDto>> GetItems(int page,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes);
    }
}

using ConstructionPlanning.DataLayer.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionPlanning.DataLayer.Repositories
{
    /// <summary>
    /// Содержит методы репозитория.
    /// </summary>
    /// <typeparam name="T">Сущности, наследуемы от <see cref="IBaseObject"/>></typeparam>
    public interface IRepository<T> 
        where T : IBaseObject
    {
        /// <summary>
        /// Возвращает список всех объектов, наследуемые от <see cref="IBaseObject"/>
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Вовзращает объект, наследуемый от <see cref="IBaseObject"/>, по ИД.
        /// </summary>
        Task<T> GetById(int id);

        /// <summary>
        /// Добавляет объект, наследуемый от <see cref="IBaseObject"/>, в БД.
        /// </summary>
        Task Add(T entity);

        /// <summary>
        /// Обновляет существующий объект, наследуемый от <see cref="IBaseObject"/>, в БД.
        /// </summary>
        Task Update(T entity);

        /// <summary>
        /// Удаляет существующий объект, наследуемый от <see cref="IBaseObject"/>, из БД.
        /// </summary>
        Task Delete(int id);
    }
}

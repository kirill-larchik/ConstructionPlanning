using ConstructionPlanning.DataAccess.DbContext;
using ConstructionPlanning.DataAccess.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionPlanning.DataAccess.Repositories
{
    /// <inheritdoc />
    public class BaseRepository<T> : IRepository<T>
        where T : class, IBaseObject
    {
        private readonly ConstructionPlanningDbContext _context;

        /// <summary>
        /// Создание экземпляра класса <see cref="BaseRepository{T}"/>
        /// </summary>
        public BaseRepository(ConstructionPlanningDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
        }

        /// <inheritdoc />
        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _context.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var dbSet = _context.Set<T>();
            if (includes.Length != 0)
            {
                IIncludableQueryable<T, object> dbSetWithInclude = dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    dbSetWithInclude = dbSetWithInclude.Include(include);
                }

                return dbSetWithInclude.AsNoTracking();
            }

            return dbSet.AsNoTracking();
        }

        /// <inheritdoc />
        public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            return await GetAll(includes).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task Update(T entity)
        {
            _context.Update(entity);
        }

        /// <inheritdoc />
        public void ClearTracker()
        {
            _context.ChangeTracker.Clear();
        }
    }
}

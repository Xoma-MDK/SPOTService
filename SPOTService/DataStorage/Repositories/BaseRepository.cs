using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

#nullable enable
namespace ControlManagerLib.DataStorage.Repositories
{
    /// <summary>
    /// Базовая реализация паттерна IRepository
    /// </summary>
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>, IDisposable
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext _context;

        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        /// <param name="context">Экземпляр класса, отнаследованного от DbContext</param>
        public BaseRepository(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить одну запись из таблицы по id
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Получить все данные из таблицы
        /// </summary>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        /// <param name="entity">Новая запись</param> 
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Удалить запись из таблицы
        /// </summary>
        /// <param name="id">Идентификатор записи</param> 
        public virtual async Task<TEntity?> DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Обновить запись в таблице
        /// </summary>
        /// <param name="entity">Запись с обновленными данными</param> 
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

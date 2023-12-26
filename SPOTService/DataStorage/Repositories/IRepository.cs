using SPOTService.DataStorage.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPOTService.DataStorage.Repositories
{
    /// <summary>
    /// Паттерн IRepository для работы с базами данных
    /// </summary>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Получить все данные из таблицы асинхронно
        /// </summary>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Получить одну запись из таблицы по id
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        Task<T?> GetAsync(int id);

        /// <summary>
        /// Добавить запись в таблицу асинхронно
        /// </summary>
        /// <param name="entity">Новая запись</param> 
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Обновить запись в таблице асинхронно
        /// </summary>
        /// <param name="entity">Запись с обновленными данными</param> 
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Удалить запись из таблицы асинхронно
        /// </summary>
        /// <param name="id">Идентификатор записи</param> 
        Task<T?> DeleteAsync(int id);
    }
}

using ControlManagerLib.DataStorage.Repositories;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage.Entities;
using Telegram.Bot.Types;

namespace SPOTService.DataStorage.Repositories
{
    public class SurveyRepository(MainContext _context) : BaseRepository<Survey, MainContext>(_context)
    {
        /// <summary>
        /// Обновить запись в таблице
        /// </summary>
        /// <param name="entity">Запись с обновленными данными</param> 
        /// <returns>
        /// Возвращает tuple, где 
        /// Item1 - запись с обновленными данными, 
        /// Item2 - флаг, было ли изменено местоположение
        /// Item3 - флаг, были ли изменены какие-либо поля
        /// </returns>
        public virtual async Task<Survey?> UpdateAsync(int id, Survey entity)
        {
            var old = await _context.Surveys.FindAsync(id);

            if (old == null)
            {
                return null;
            }
            else
            {
                old.Title = entity.Title;
                old.Description = entity.Description;
                old.AccessCode = entity.AccessCode;
                old.StartTime = entity.StartTime;
                old.EndTime = entity.EndTime;
                old.Active = entity.Active;
                old.GroupId = entity.GroupId;
                old.Department = entity.Department;
                old.UserId = entity.UserId;

                old.Questions = entity.Questions;
                

                bool isModified = (_context.Entry(old).State == EntityState.Modified);
                _context.Surveys.Update(old);
                await _context.SaveChangesAsync();

                return old;
            }
        }
    }
}

using ControlManagerLib.DataStorage.Repositories;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage.Entities;
using Telegram.Bot.Types;

namespace SPOTService.DataStorage.Repositories
{
    public class SurveyRepository(MainContext _context, ILogger<SurveyRepository> _logger) : BaseRepository<Survey, MainContext>(_context)
    {
        /// <summary>
        /// Метод обновления поля Active
        /// </summary>
        /// <param name="id"></param>
        /// <param name="survey"></param>
        /// <returns></returns>
        public virtual async Task<Survey?> UpdateActiveAsync(int id, Survey survey)
        {
            var old = await _context.Surveys.FindAsync(id);

            if (old == null)
            {
                return null;
            }
            else
            {
                old.Active = survey.Active;
            }

            try
            {
                _context.Update(old);
                await _context.SaveChangesAsync();
                return old;
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Crit: {}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Обновить запись в таблице
        /// </summary>
        /// <param name="entity">Запись с обновленными данными</param> 
        /// <returns>
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
                old.CreatorId = entity.CreatorId;

                }

                try
                {
                    bool isModified = (_context.Entry(old).State == EntityState.Modified);
                    _context.Surveys.Update(old);
                    await _context.SaveChangesAsync();

                    var newEntity = await _context.Surveys.FindAsync(id);
                    return newEntity;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Crit: {}", ex.Message);
                    return null;
                }
            }
        }
    }


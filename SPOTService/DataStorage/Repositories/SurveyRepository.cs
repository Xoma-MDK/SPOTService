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
                old.UserId = entity.UserId;

                foreach (var question in old.Questions!)
                {
                    if (entity.Questions!.Select(q => q.Id).ToArray().Contains(question.Id))
                    {
                        old.Questions.First(q => q.Id == question.Id).Title = entity.Questions!.First(q => q.Id == question.Id).Title;
                        old.Questions.First(q => q.Id == question.Id).IsOpen = entity.Questions!.First(q => q.Id == question.Id).IsOpen;
                        foreach(AnswerVariant answerVariant in old.Questions!.First(q => q.Id == question.Id).AnswerVariants!)
                        {
                            if (entity.Questions!.First(q => q.Id == question.Id).AnswerVariants!.Select(q => q.Id).ToArray().Contains(answerVariant.Id))
                            {
                                old.Questions!.First(q => q.Id == question.Id).AnswerVariants!.First(a => a.Id == answerVariant.Id).Title = entity.Questions!.First(q => q.Id == question.Id).AnswerVariants!.First(a => a.Id == answerVariant.Id).Title;
                            }
                            else
                            {
                                old.Questions!.First(q => q.Id == question.Id).AnswerVariants!.ToList().Remove(answerVariant);
                                _context.AnswerVariants.Remove(answerVariant);
                                await _context.SaveChangesAsync();
                            }
                        }
                        foreach (var answerVariant in entity.Questions!.First(q => q.Id == question.Id).AnswerVariants!.Where(q => q.Id == 0).Select(q => new AnswerVariant() { Title = q.Title}))
                        {
                            var answerVariantNew = _context.AnswerVariants.Add(answerVariant);
                            await _context.SaveChangesAsync();

                            _context.QuestionAnswerVariants.Add(new QuestionAnswerVariant() { QuestionId = question.Id, AnswerVariantId = answerVariantNew.Entity.Id });
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        old.Questions.ToList().Remove(question);
                        _context.Questions.Remove(question);
                    }
                }

                try
                {
                    bool isModified = (_context.Entry(old).State == EntityState.Modified);
                    _context.Surveys.Update(old);
                    await _context.SaveChangesAsync();

                    foreach (var question in entity.Questions!.Where(q => q.Id == 0).Select(q => new Question() { Title = q.Title, IsOpen = q.IsOpen, AnswerVariants = q.AnswerVariants }))
                    {
                        var q = _context.Questions.Add(question);
                        await _context.SaveChangesAsync();
                        
                        _context.SurveyQuestions.Add(new SurveyQuestion() { QuestionId = q.Entity.Id, SurveyId = id });
                        await _context.SaveChangesAsync();
                    }
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
}

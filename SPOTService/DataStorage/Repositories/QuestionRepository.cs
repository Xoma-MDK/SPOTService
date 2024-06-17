using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью Question.
    /// </summary>
    public class QuestionRepository(MainContext context) : BaseRepository<Question, MainContext>(context)
    {
    }
}

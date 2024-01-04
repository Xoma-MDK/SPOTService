using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    public class QuestionRepository(MainContext context) : BaseRepository<Question, MainContext>(context)
    {
    }
}

using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью AnswerVariant.
    /// </summary>
    public class AnswerVariantRepository(MainContext context) : BaseRepository<AnswerVariant, MainContext>(context)
    {
    }
}

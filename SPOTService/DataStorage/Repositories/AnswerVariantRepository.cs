using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    public class AnswerVariantRepository(MainContext context) : BaseRepository<AnswerVariant, MainContext>(context)
    {
    }
}

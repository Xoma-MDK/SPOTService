using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    public class SurveyRepository(MainContext context) : BaseRepository<Survey, MainContext>(context)
    {

    }
}

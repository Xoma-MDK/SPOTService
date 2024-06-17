using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью Group.
    /// </summary>
    public class GroupRepository(MainContext context) : BaseRepository<Group, MainContext>(context)
    {

    }
}

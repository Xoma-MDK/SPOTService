using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    public class GroupRepository(MainContext context) : BaseRepository<Group, MainContext>(context)
    {

    }
}

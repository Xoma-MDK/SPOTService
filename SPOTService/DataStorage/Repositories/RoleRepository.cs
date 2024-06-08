using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    public class RoleRepository(MainContext context) : BaseRepository<Role, MainContext>(context)
    {
    }
}

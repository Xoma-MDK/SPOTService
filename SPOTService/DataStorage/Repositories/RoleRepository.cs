using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью Role.
    /// </summary>
    public class RoleRepository(MainContext context) : BaseRepository<Role, MainContext>(context)
    {
    }
}

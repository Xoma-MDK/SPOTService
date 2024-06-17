using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью пользователь.
    /// </summary>
    public class UserRepository(MainContext context) : BaseRepository<User, MainContext>(context)
    {

    }
}

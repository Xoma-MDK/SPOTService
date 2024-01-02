using ControlManagerLib.DataStorage.Repositories;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Repositories
{
    public class UserRepository(MainContext context) : BaseRepository<User, MainContext>(context)
    {

    }
}

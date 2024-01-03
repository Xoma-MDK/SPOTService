using Microsoft.AspNetCore.Mvc;

namespace SPOTService.Controllers
{
    public class UserController(ILogger<UserController> logger) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
    }
}

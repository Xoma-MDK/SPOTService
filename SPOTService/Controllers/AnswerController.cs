using Microsoft.AspNetCore.Mvc;
using SPOTService.DataStorage;

namespace SPOTService.Controllers
{
    public class AnswerController : ControllerBase
    {
        private readonly MainContext _mainContext;
        private readonly ILogger<AnswerController> _logger;
        public AnswerController(MainContext mainContext, ILogger<AnswerController> logger)
        {
            _mainContext = mainContext;
            _logger = logger;
        }
        public ActionResult Ping() {
            _logger.LogWarning("hsdfhsdfhosdf");
            return Ok();

        }
    }
}

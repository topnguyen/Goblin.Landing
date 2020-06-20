using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class WorkController : BaseController
    {
        [Route(Endpoints.Work)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
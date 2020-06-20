using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class HomeController : BaseController
    {
        [Route(Endpoints.Home)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
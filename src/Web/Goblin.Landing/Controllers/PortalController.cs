using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class PortalController : BaseAuthController
    {
        [Route(Endpoints.Portal)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }  
    }
}
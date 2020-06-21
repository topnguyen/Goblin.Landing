using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class ContactController : BaseController
    {
        [Route(Endpoints.Contact)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
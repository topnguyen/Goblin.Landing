using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class MemberController : BaseController
    {
        
        [Route(Endpoints.Member)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
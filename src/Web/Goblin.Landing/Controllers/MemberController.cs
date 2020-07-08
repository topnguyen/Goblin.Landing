using Goblin.Landing.Core.Constants;
using Goblin.Landing.Models.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class MemberController : BaseAuthController
    {
        
        [Auth("Member Manager")]
        [Route(Endpoints.Member)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
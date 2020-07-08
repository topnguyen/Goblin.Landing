using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class PortalController : BaseAuthController
    {
        [Route(Endpoints.Profile)]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }  
        
        [Route(Endpoints.Account)]
        [HttpGet]
        public IActionResult Account()
        {
            return View();
        } 
    }
}
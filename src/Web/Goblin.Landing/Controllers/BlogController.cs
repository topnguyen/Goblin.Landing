using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class BlogController : BaseController
    {
        [Route(Endpoints.Blog)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
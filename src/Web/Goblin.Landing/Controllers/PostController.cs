using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class PostController : BaseController
    {
        [Route(Endpoints.Post)]
        [HttpGet]
        public IActionResult Index([FromRoute]string slug)
        {
            return View();
        }
    }
}
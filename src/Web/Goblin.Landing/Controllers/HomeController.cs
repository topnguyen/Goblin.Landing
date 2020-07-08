using System.Threading;
using System.Threading.Tasks;
using Goblin.Landing.Core.Constants;
using Goblin.Landing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class HomeController : BaseController
    {
        [Route(Endpoints.Home)]
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var blogModel = await BlogService.GetBlogAsync(1, cancellationToken).ConfigureAwait(true);
            
            return View(blogModel);
        }   
        
        [Route(Endpoints.Error)]
        [HttpGet]
        public IActionResult Error(int code)
        {
            ViewBag.ErrorCode = code;
            
            return View("Error");
        }
    }
}
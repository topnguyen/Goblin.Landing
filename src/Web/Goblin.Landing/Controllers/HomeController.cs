using System.Threading;
using System.Threading.Tasks;
using Goblin.Landing.Contract.Service;
using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        
        [Route(Endpoints.Home)]
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var blogModel = await _blogService.GetBlog(1, cancellationToken).ConfigureAwait(true);
            
            return View(blogModel);
        }   
        
        [Route(Endpoints.Error)]
        [HttpGet]
        public IActionResult Error(int code)
        {
            if (code == StatusCodes.Status404NotFound)
            {
                return View("NotFound");
            }
            
            return View("NotFound");
        }
    }
}
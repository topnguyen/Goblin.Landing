using System.Threading;
using System.Threading.Tasks;
using Goblin.Landing.Contract.Service;
using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        
        [Route(Endpoints.Blog)]
        [HttpGet]
        public async Task<IActionResult> Index(int? pageNo, CancellationToken cancellationToken = default)
        {
            pageNo ??= 1;

            var blogModel = await _blogService.GetBlog(pageNo.Value, cancellationToken).ConfigureAwait(true);
            
            return View(blogModel);
        }
    }
}
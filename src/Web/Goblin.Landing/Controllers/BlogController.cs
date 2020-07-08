using System.Threading;
using System.Threading.Tasks;
using Goblin.Landing.Core.Constants;
using Goblin.Landing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class BlogController : BaseController
    {
        [Route(Endpoints.Blog)]
        [HttpGet]
        public async Task<IActionResult> Index(int? pageNo, CancellationToken cancellationToken = default)
        {
            pageNo ??= 1;

            var blogModel = await BlogService.GetBlogAsync(pageNo.Value, cancellationToken).ConfigureAwait(true);
            
            return View(blogModel);
        }
    }
}
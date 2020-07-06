using System;
using System.Linq;
using Elect.DI.Attributes;
using Goblin.Landing.Contract.Service;
using System.Threading;
using System.Threading.Tasks;
using Goblin.BlogCrawler.Share;
using Goblin.BlogCrawler.Share.Models;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Service
{
    [ScopedDependency(ServiceType = typeof(IBlogService))]
    public class BlogService : Base.Service, IBlogService
    {
        public async Task<BlogModel> GetBlog(int pageNo, CancellationToken cancellationToken = default)
        {
            var blogModel = new BlogModel
            {
                CurrentPageNo = pageNo
            };

            if (blogModel.CurrentPageNo > 1)
            {
                blogModel.PreviousPageNo = blogModel.CurrentPageNo - 1;
            }
            
            var take = 15;

            var skip = (pageNo - 1) * take;

            var blogCrawlerGetPagedPostModel = new GoblinBlogCrawlerGetPagedPostModel
            {
                Skip = skip,
                Take = take,
                SortBy = nameof(GoblinBlogCrawlerPostModel.PublishTime),
                IsSortAscending = false
            };

            var response =
                await GoblinBlogCrawlerHelper
                    .GetPagedAsync(blogCrawlerGetPagedPostModel, cancellationToken)
                    .ConfigureAwait(true);
            
            blogModel.Posts = response.Items.ToList();
            
            blogModel.TotalPost = response.Total;
            
            blogModel.TotalPage = (int) Math.Ceiling((double) response.Total / take) - 1;

            if (blogModel.CurrentPageNo < blogModel.TotalPage)
            {
                blogModel.NextPageNo = blogModel.CurrentPageNo + 1;
            }
            
            return blogModel;
        }
    }
}
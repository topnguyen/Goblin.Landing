using System.Collections.Generic;
using Goblin.BlogCrawler.Share.Models;

namespace Goblin.Landing.Core.Models
{
    public class BlogModel
    {
        public List<GoblinBlogCrawlerPostModel> Posts { get; set; }

        public long TotalPost { get; set; }
        
        public long TotalPage { get; set; }

        public int? PreviousPageNo { get; set; }
        
        public int CurrentPageNo { get; set; }
        
        public int? NextPageNo { get; set; }
    }
}
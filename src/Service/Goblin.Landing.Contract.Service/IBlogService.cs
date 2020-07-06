using System.Threading;
using System.Threading.Tasks;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Contract.Service
{
    public interface IBlogService
    {
        Task<BlogModel> GetBlog(int pageNo, CancellationToken cancellationToken = default);
    }
}
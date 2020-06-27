using System.Threading;
using System.Threading.Tasks;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Contract.Service
{
    public interface ISampleService
    {
        Task<SampleModel> CreateAsync(CreateSampleModel model, CancellationToken cancellationToken = default);
        
        Task<SampleModel> GetAsync(long id, CancellationToken cancellationToken = default);
        
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
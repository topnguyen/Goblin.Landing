using System.Threading.Tasks;
using Goblin.Landing.Contract.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Goblin.Landing
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Goblin.Core.Web.Setup.ProgramHelper.Main(args, webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                }, scope =>
                {
                    var infrastructureBootstrapper = scope.ServiceProvider.GetService<IBootstrapperService>();
                    
                    infrastructureBootstrapper.InitialAsync().Wait();
                }
            );
        }
    }
}
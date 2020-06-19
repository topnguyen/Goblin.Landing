using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Goblin.Landing
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Goblin.Core.Web.Setup.ProgramHelper.Main(args, webHostBuilder =>
                {
                    // https://stackoverflow.com/questions/58862932/blazor-server-static-files-dont-link-in-non-dev-environments
                    webHostBuilder.UseStaticWebAssets();
                    
                    webHostBuilder.UseStartup<Startup>();
                }, scope =>
                {
                    // Initial Database
                    //
                    // var infrastructureBootstrapper = scope.ServiceProvider.GetService<IBootstrapper>();
                    //
                    // infrastructureBootstrapper.InitialAsync().Wait();
                }
            );
        }
    }
}
using Elect.Core.ConfigUtils;
using Goblin.Core.Web.Setup;
using Goblin.Landing.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Goblin.Landing
{
    public class Startup : BaseBlazorStartup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration) : base(env, configuration)
        {
            BeforeConfigureServices = services =>
            {
                // Setting

                SystemSetting.Current = Configuration.GetSection<SystemSetting>("Setting");
            };
        }
    }
}
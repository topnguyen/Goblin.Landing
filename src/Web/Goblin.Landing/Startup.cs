using Elect.Core.ConfigUtils;
using Goblin.BlogCrawler.Share;
using Goblin.Core.Web.Setup;
using Goblin.Landing.Core.Validators;
using Goblin.Landing.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Goblin.Landing
{
    public class Startup : BaseUiStartup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration) : base(env, configuration)
        {
            RegisterValidators.Add(typeof(IValidator));

            BeforeConfigureServices = services =>
            {
                // Setting

                SystemSetting.Current = Configuration.GetSection<SystemSetting>("Setting");
                
                // Blog Crawler Service
                
                GoblinBlogCrawlerHelper.Domain = SystemSetting.Current.BlogCrawlerServiceDomain;
                
                GoblinBlogCrawlerHelper.AuthorizationKey = SystemSetting.Current.BlogCrawlerServiceAuthorizationKey;
            };
            
            BeforeUseMvc = (app, env, lifetime) =>
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            };
        }
    }
}
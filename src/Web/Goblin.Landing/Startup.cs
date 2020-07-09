using Elect.Core.ConfigUtils;
using Goblin.BlogCrawler.Share;
using Goblin.Core.Web.Setup;
using Goblin.Identity.Share;
using Goblin.Landing.Core.Validators;
using Goblin.Landing.Core;
using Goblin.Notification.Share;
using Goblin.Resource.Share;
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
            RegisterValidators.Add(typeof(BlogCrawler.Share.Validators.IValidator));
            RegisterValidators.Add(typeof(Identity.Share.Validators.IValidator));
            RegisterValidators.Add(typeof(Notification.Share.Validators.IValidator));

            BeforeConfigureServices = services =>
            {
                // Setting

                SystemSetting.Current = Configuration.GetSection<SystemSetting>("Setting");
                
                // Blog Crawler Service
                
                GoblinBlogCrawlerHelper.Domain = SystemSetting.Current.BlogCrawlerServiceDomain;
                
                GoblinBlogCrawlerHelper.AuthorizationKey = SystemSetting.Current.BlogCrawlerServiceAuthorizationKey;
                
                // Identity Service
                
                GoblinIdentityHelper.Domain = SystemSetting.Current.IdentityServiceDomain;
                
                GoblinIdentityHelper.AuthorizationKey = SystemSetting.Current.IdentityServiceAuthorizationKey;
                
                // Notification Service
                
                GoblinNotificationHelper.Domain = SystemSetting.Current.NotificationServiceDomain;
                
                GoblinNotificationHelper.AuthorizationKey = SystemSetting.Current.NotificationServiceAuthorizationKey;
                
                // Resource Service
                
                GoblinResourceHelper.Domain = SystemSetting.Current.ResourceServiceDomain;
                
                GoblinResourceHelper.AuthorizationKey = SystemSetting.Current.ResourceServiceAuthorizationKey;
            };

            BeforeUseMvc = (app, env, lifetime) =>
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            };
        }
    }
}
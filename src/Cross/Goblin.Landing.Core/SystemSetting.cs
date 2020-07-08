namespace Goblin.Landing.Core
{
    public class SystemSetting
    {
        public static SystemSetting Current { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string GoogleMapKey { get; set; }
        
        // Blog Crawler

        public string BlogCrawlerServiceDomain { get; set; }

        public string BlogCrawlerServiceAuthorizationKey { get; set; }
        
        // Identity
        
        public string IdentityServiceDomain { get; set; }

        public string IdentityServiceAuthorizationKey { get; set; }
        
        // Notification

        public string NotificationServiceDomain { get; set; }

        public string NotificationServiceAuthorizationKey { get; set; }
    }
}
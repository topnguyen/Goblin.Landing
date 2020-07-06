namespace Goblin.Landing.Core.Constants
{
    public static class Endpoints
    {
        public const string Home = "~/";
        
        public const string Error = "~/error/{code}";

        public const string Work = "~/work";
        
        public const string Member = "~/member";
        
        public const string Blog = "~/blog/{pageNo?}";
        
        public const string Post = "~/blog/{slug}";
        
        public const string Contact = "~/contact";

        public const string Login = "~/login";
        
        public const string Logout = "~/logout";
    }
}
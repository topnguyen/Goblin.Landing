namespace Goblin.Landing.Core
{
    public class SystemSetting
    {
        public static SystemSetting Current { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }
    }
}
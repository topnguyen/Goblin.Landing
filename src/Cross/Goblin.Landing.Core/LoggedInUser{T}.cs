using Goblin.Core.Web.Utils;

namespace Goblin.Landing.Core
{
    public class LoggedInUser<T> where T:class
    {
        public T Data { get; set; }

        public LoggedInUser()
        {
        }

        public LoggedInUser(T data)
        {
            Data = data;
        }

        public static LoggedInUser<T> Current
        {
            get => Get();
            set => Set(value);
        }

        private static LoggedInUser<T> Get()
        {
            return SingletonPerHttpRequest<LoggedInUser<T>>.Current;
        }

        private static void Set(LoggedInUser<T> value)
        {
            SingletonPerHttpRequest<LoggedInUser<T>>.Current = value;
        }
    }
}
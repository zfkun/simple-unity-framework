using Suf.Utils;

namespace Suf.Base
{
    /// <summary>
    /// 普通单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T>: ISingleton where T : new()
    {
        private static readonly object _mutex = new object();

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_mutex)
                {
                    _instance ??= new T();
                }

                return _instance;
            }
        }
        
        public static bool IsInit => _instance != null;
        
        public virtual void Init()
        {
            LogUtils.Info($"[{typeof(T).Name}] Init");
        }
    }
}
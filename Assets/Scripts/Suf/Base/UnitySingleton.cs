using UnityEngine;

namespace Suf.Base
{
    /// <summary>
    /// Unity单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitySingleton<T> : MonoBehaviour where T : UnitySingleton<T>
    {
        private static T _instance;
        protected static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
        
        public virtual void Init()
        {
            Debug.Log($"[{name}] Init");
        }
    }
}
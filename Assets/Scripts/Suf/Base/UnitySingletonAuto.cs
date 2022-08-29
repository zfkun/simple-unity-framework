using UnityEngine;

namespace Suf.Base
{
    /// <summary>
    /// Unity单例, 自动创建挂载物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitySingletonAuto<T> : MonoBehaviour where T : UnitySingletonAuto<T>
    {
        // protected static bool autoCreateGameObject = true;
        // protected static bool dontDestroyOnLoad = true;
        
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;

                _instance = new GameObject(typeof(T).Name).AddComponent<T>();

                return _instance;
            }
        }
        
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
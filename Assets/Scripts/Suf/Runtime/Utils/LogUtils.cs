using System.Diagnostics;

namespace Suf.Utils
{
    public static class LogUtils
    {
        [Conditional("ENABLE_LOG")]
        public static void Info(object message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.Log("[I] " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") +" " + message, context);
        }
        
        [Conditional("ENABLE_LOG")]
        public static void InfoFormat(string format, params object[]args)
        {
            UnityEngine.Debug.LogFormat(format, args);
        }
        
        [Conditional("ENABLE_LOG")]
        public static void Warn(object message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogWarning("[W] " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") +" " + message, context);
        }
        
        [Conditional("ENABLE_LOG")]
        public static void WarningFormat(string format, params object[]args)
        {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }
        
        [Conditional("ENABLE_LOG")]
        public static void Error(object message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogError("[E] " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") +" " + message, context);
        }
        
        [Conditional("ENABLE_LOG")]
        public static void ErrorFormat(string format, params object[]args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }
    }
}
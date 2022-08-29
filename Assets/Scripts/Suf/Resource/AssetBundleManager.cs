using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Suf.Config;
using Suf.Utils;

namespace Suf.Resource
{
    public class AssetBundleLoader: Base.Singleton<AssetBundleLoader>
    {
        private Dictionary<string, AssetBundle> _bundles;

        public override void Init()
        {
            base.Init();
            _bundles = new Dictionary<string, AssetBundle>();
        }


        #region 资源操作

        /// <summary>
        ///  加载单个资源
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadAsset<T>(string bundleName, string assetName) where T : Object
        {
            if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(assetName)) return null;

#if UNITY_EDITOR
            if (ResourceConfig.EditorMode)
            {
                var path = AssetBundleUtils.GetAssetPath(bundleName, assetName);
                return AssetDatabase.LoadAssetAtPath<T>(path);
            }
#endif
            
            var ab = LoadBundle(AssetBundleUtils.GetAssetPath(bundleName, assetName));
            return ab == null ? null : ab.LoadAsset<T>(assetName);
        }

        /// <summary>
        /// 加载所有资源
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public UnityEngine.Object[] LoadAssets(string bundleName)
        {
            var path = AssetBundleUtils.GetBundlePath(bundleName);
            var ab = LoadBundle(path);
            return ab == null ? null : ab.LoadAllAssets();
        }
        
        /// <summary>
        /// 加载指定类型的所有资源
        /// </summary>
        /// <param name="bundleName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] LoadAssets<T>(string bundleName) where T : Object
        {
            var path = AssetBundleUtils.GetBundlePath(bundleName);
            var ab = LoadBundle(path);
            return ab == null ? null : ab.LoadAllAssets<T>();
        }
        
        #endregion



        #region AssetBundle 操作

        private static AssetBundle LoadBundle(string path)
        {
            if (UnityEngine.Application.platform == RuntimePlatform.Android)
            {
                path = path.Replace(@"jar:file://", "");
                path = path.Replace("apk!/assets", "apk!assets");
            }

            var ab = AssetBundle.LoadFromFile(path);
            if (ab == null)
            {
                LogUtils.ErrorFormat("LoadBundle {0} fail", path);
            }

            return ab;
        }
        
        private AssetBundle LoadBundleWithDependence(string bundleName)
        {
            bundleName = bundleName.ToLower();
            
            // 缓存检查
            if (IsLoadedBundle(bundleName)) return _bundles[bundleName];
            
            // TODO: 加载依赖
            // string[] deps = new[] {""}; 
            List<string> bundleNames = new List<string>();
            bundleNames.Add(bundleName);
            foreach (var name in bundleNames)
            {
                if (IsLoadedBundle(name)) continue;

                var bundle = LoadBundle(AssetBundleUtils.GetBundlePath(name));
                if (bundle != null) _bundles.Add(name, bundle);
            }

            if (_bundles.TryGetValue(bundleName, out var ab)) return ab;

            LogUtils.ErrorFormat("LoadBundle {0} fail", bundleName);
            return null;
        }

        public bool IsLoadedBundle(string bundleName)
        {
#if UNITY_EDITOR
            return true;
#endif
            return _bundles.ContainsKey(bundleName);
        }
        
        #endregion
    }
}
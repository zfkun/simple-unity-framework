using System.IO;
using Suf.Config;

namespace Suf.Utils
{
    public static class AssetBundleUtils
    {
        public static string GetBundlePath(string bundleName)
        {
            return Path.Combine(PathUtils.DataPath(), bundleName);
        }
        
        public static string GetAssetPath(string bundleName, string assetName)
        {
#if UNITY_EDITOR
            if (ResourceConfig.EditorMode)
            {
                return Path.Combine(PathUtils.DataFilePath(bundleName), assetName);
            }
#endif
            
            return Path.Combine(PathUtils.DataPath(), bundleName, assetName);
        }
    }
}

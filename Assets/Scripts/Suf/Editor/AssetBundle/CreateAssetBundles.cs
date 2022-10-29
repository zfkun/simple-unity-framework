using System.IO;
using UnityEditor;

using Suf.Utils;

public class CreateAssetBundles
{
    [MenuItem("AssetBundle/Open Build Folder")]
    public static void OpenBuildFolder()
    {
        
    }

    [MenuItem("AssetBundle/Build All")]
    public static void BuildAll()
    {
        const string dir = "./AssetBundles/osx";
        if (!Directory.Exists(dir))
        {
            var info = Directory.CreateDirectory(dir);
            LogUtils.Info("创建资源目录: " + info);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);
        LogUtils.Info("打包完成");
    }
}

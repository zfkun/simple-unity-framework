using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles
{
    [MenuItem("AssetBundle/Build All")]
    public static void BuildAll()
    {
        const string dir = "AssetBundles";
        if (!Directory.Exists(dir))
        {
            var info = Directory.CreateDirectory(dir);
            Debug.Log("创建资源目录: " + info);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);
        Debug.Log("打包完成");
    }
}

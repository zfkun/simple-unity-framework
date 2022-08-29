namespace Suf.Resource
{
    public enum LoadType
    {
        /// <summary>
        /// 基于 Resources 文件夹加载 
        /// </summary>
        Resources = 1,

        /// <summary>
        /// 基于 AssetBundle 加载
        /// </summary>
        AssetBundle = 2,

        /// <summary>
        /// 基于 Addressable 加载 
        /// </summary>
        Addressable = 3,
    }
}
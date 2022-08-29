namespace Suf.Utils
{
    public static class PathUtils
    {
        /// <summary>
        /// 数据目录 ()
        /// - webgl
        ///     - /idbfs/<md5 hash of data path>
        /// - ios
        ///     - /var/mobile/Containers/Data/Application/<guid>/Documents
        /// - android
        ///     - /storage/emulated/0/Android/data/<package name>/files
        /// - mac:
        ///     - ~/Library/Application Support/<company name>/<product name>
        ///     - ~/Library/Caches
        ///     - ~/Library/Application Support/unity.<company name>.<product name>
        /// - windows
        ///     - C:\Users\<username>\AppData\LocalLow\<company name>\<product name>
        /// - linux
        ///     - $XDG_CONFIG_HOME/unity3d
        ///     - $HOME/.config/unity3d
        /// - windows store apps
        ///     - %userprofile%\AppData\Local\Packages\<product name>\LocalState
        /// - Windows Editor and Standalone Player
        ///     - %userprofile%\AppData\LocalLow\<company name>\<product name>
        /// </summary>
        /// <returns></returns>
        public static string DataPath()
        {
            return System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, PlatformUtils.Name());
        }
        
        /// <summary>
        /// 内置数据目录
        /// - ios
        ///     - /var/containers/Bundle/Application/app sandbox/test.app/Data/Raw
        /// - android: jar:file:///data/app/package name-1/base.apk!/assets
        /// - mac
        /// - windows: appname_Data/StreamingAssets
        /// </summary>
        /// <returns></returns>
        public static string StreamingPath()
        {
            return System.IO.Path.Combine(UnityEngine.Application.streamingAssetsPath, PlatformUtils.Name());
        }
        
        /// <summary>
        /// 本地资源文件路径
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string DataFilePath(string filename)
        {
            return System.IO.Path.Combine(DataPath(), filename);
        }

        public static string PackagePath()
        {
            return System.IO.Path.Combine("Art");
        }
    }
}
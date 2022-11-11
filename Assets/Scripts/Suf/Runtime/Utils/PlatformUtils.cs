using UnityEngine;

namespace Suf.Utils
{
    public static class PlatformUtils
    {
        private static string _name = null;
        public static string Name()
        {
            if (_name == null)
            {
#if UNITY_EDITOR
                return UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString();
#else
                switch (UnityEngine.Application.platform)
                {
                    case RuntimePlatform.OSXEditor:
                    case RuntimePlatform.OSXPlayer:
                        _name = "StandaloneOSX";
                        break;
                    
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.WindowsEditor:
#if UNITY_64
                        _name = "StandaloneWindows64";
#else
                        _name = "StandaloneWindows";
#endif
                        break;
                    
                    case RuntimePlatform.IPhonePlayer:
                        _name = "iOS";
                        break;
                    
                    case RuntimePlatform.LinuxPlayer:
                    case RuntimePlatform.LinuxEditor:
                        _name = "StandaloneLinux64";
                        break;
                    
                    case RuntimePlatform.WebGLPlayer:
                        _name = "WebGL";
                        break;
                    
                    case RuntimePlatform.WSAPlayerX86:
                    case RuntimePlatform.WSAPlayerX64:
                    case RuntimePlatform.WSAPlayerARM:
                        _name = "WSAPlayer";
                        break;
                    
                    case RuntimePlatform.Android:
                    case RuntimePlatform.PS4:
                    case RuntimePlatform.XboxOne:
                    case RuntimePlatform.tvOS:
                    case RuntimePlatform.Switch:
                    //case RuntimePlatform.Lumin:
                    //case RuntimePlatform.Stadia:
                        _name = UnityEngine.Application.platform.ToString();
                    break;
                }

                _name =  UnityEngine.Application.platform.ToString();
#endif
            }

            return _name;
        }
    }
}
using Suf.Resource;
using Suf.Utils;
using UnityEngine;

namespace Suf.UI
{
    public class Panel : MonoBehaviour, IPanel
    {
        public virtual void OnInit()
        {
        }
        
        public virtual void OnClose()
        {
        }

        public virtual void OnPause()
        {
            
        }

        public virtual void OnResume()
        {
        }


        public virtual void Hide()
        {
            // // 由 Addressables 直接实例化的资源, 必须由 Addressables 释放
            // LogUtils.InfoFormat("[UIManager] 释放UI资源: key={0}, layer={1}", key, layer);
            // var ok = AddressableManager.Instance.Release(gameObject);
            // LogUtils.InfoFormat("[UIManager] 释放结果: key={0}, ok={1}", key, ok);
            //
            // // 手工实例化的资源, 手工释放
            // // Destroy(panel);
            // var a = new Panel();
            // gameObject.AddComponent(a);
        }
    }
}

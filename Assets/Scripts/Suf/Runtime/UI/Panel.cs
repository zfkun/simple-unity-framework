using UnityEngine;

namespace Suf.UI
{
    public abstract class Panel : MonoBehaviour, IPanel
    {
        private UIData _ui;

        public string uiKey => _ui.key;
        public LayerType uiLayerType => _ui.layer;
        
        public virtual void OnInit(UIData ui)
        {
            _ui = ui;
            // LogUtils.InfoFormat("[{0}] OnInit: {1}", GetType().FullName, data);
        }

        public virtual void OnOpen()
        {
            // LogUtils.InfoFormat("[{0}] OnOpen", GetType().FullName);
        }

        public virtual void OnClose()
        {
            // LogUtils.InfoFormat("[{0}] OnClose", GetType().FullName);
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }
    }
}

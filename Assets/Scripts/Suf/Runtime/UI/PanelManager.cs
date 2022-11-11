using System;

using Suf.Base;

namespace Suf.UI
{
    public class PanelManager: UnitySingletonAuto<PanelManager>
    {
        // private Dictionary<LayerType, Stack<Panel>> _panels;

        public T ShowPanel<T>(string key, LayerType layer = LayerType.Back) where T: Panel
        {
            var obj = UIManager.Instance.ShowUI(key, layer);
            if (obj != null)
            {
                if (!obj.TryGetComponent<T>(out var panel))
                {
                    panel = obj.AddComponent<T>();
                    panel.OnInit(new UIData(key, layer));
                }
                
                // PushPanel(panel);
                panel.OnOpen();

                return panel;
            }

            return null;
        }

        public void ShowPanel<T>(object key, Action<T> callback, LayerType layer = LayerType.Back) where T : Panel
        {
            UIManager.Instance.ShowUI(key, o =>
            {
                if (!o.TryGetComponent<T>(out var panel))
                {
                    panel = o.AddComponent<T>();
                    panel.OnInit(new UIData(key.ToString(), layer));
                }
                
                // PushPanel(panel);
                panel.OnOpen();

                callback(panel);
            }, layer);
        }

        public bool HidePanel(string key, bool release = false)
        {
            var p = UIManager.Instance.HideUI(key);
            if (p == null) return false;

            if (p.TryGetComponent<Panel>(out var panel))
            {
                panel.OnClose();
            }

            if (release)
            {
                return UIManager.Instance.ReleaseUI(key);
            }

            return true;
        }


        // private bool PushPanel(Panel panel)
        // {
        //     _panels ??= new Dictionary<LayerType, Stack<Panel>>();
        //
        //     if (!_panels.TryGetValue(panel.uiLayerType, out var ps))
        //     {
        //         ps = new Stack<Panel>();
        //         _panels.Add(panel.uiLayerType, ps);
        //     }
        //
        //     if (ps.Contains(panel)) return false;
        //     ps.Push(panel);
        //
        //     panel.OnOpen();
        //
        //     return true;
        // }
        //
        // private bool PopPanel(LayerType layer, bool release = false)
        // {
        //     _panels ??= new Dictionary<LayerType, Stack<Panel>>();
        //
        //     if (!_panels.TryGetValue(layer, out var ps)) return false;
        //     if (ps.Count <= 0) return false;
        //
        //     var panel = ps.Pop();
        //     panel.OnClose();
        //
        //     if (release) return UIManager.Instance.ReleaseUI(panel.uiKey);
        //
        //     return true;
        // }
    }
}
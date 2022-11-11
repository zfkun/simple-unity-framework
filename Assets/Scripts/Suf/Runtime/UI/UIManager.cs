using System;
using System.Collections.Generic;

using UnityEngine;

using Suf.Base;
using Suf.Resource;
using Suf.Utils;

namespace Suf.UI
{
    public sealed class UIManager: UnitySingletonAuto<UIManager>
    {
        private GameObject _root;
        private Dictionary<LayerType, Transform> _layers;
        private Dictionary<string, GameObject> _cache;
        // private Dictionary<LayerType, Stack<Panel>> _panels;

        public GameObject root => _root;
        public Transform backLayer => _layers?[LayerType.Back];
        public Transform middleLayer => _layers?[LayerType.Middle];
        public Transform frontLayer => _layers?[LayerType.Front];
        public Transform systemLayer => _layers?[LayerType.System];
        
        public override void Init()
        {
            base.Init();

            if (_root != null) return;
            _root = GameObject.Find("Canvas");

            if (_root == null)
            {
                LogUtils.ErrorFormat("[UIManager] root invalid");
                return;
            }

            _layers = new Dictionary<LayerType, Transform>
            {
                [LayerType.Back] = _root.transform.Find("Back"),
                [LayerType.Middle] = _root.transform.Find("Middle"),
                [LayerType.Front] = _root.transform.Find("Front"),
                [LayerType.System] = _root.transform.Find("System")
            };

            if (backLayer == null)
            {
                LogUtils.ErrorFormat("[UIManager] layer invalid: {0}", LayerType.Back);
                return;
            }
            if (middleLayer == null)
            {
                LogUtils.ErrorFormat("[UIManager] layer invalid: {0}", LayerType.Middle);
                return;
            }
            if (frontLayer == null)
            {
                LogUtils.ErrorFormat("[UIManager] layer invalid: {0}", LayerType.Front);
                return;
            }
            if (systemLayer == null)
            {
                LogUtils.ErrorFormat("[UIManager] layer invalid: {0}", LayerType.System);
                return;
            }

            _cache = new Dictionary<string, GameObject>();

            // _panels = new Dictionary<LayerType, Stack<Panel>>
            // {
            //     [LayerType.Back] = new Stack<Panel>(),
            //     [LayerType.Middle] = new Stack<Panel>(),
            //     [LayerType.Front] = new Stack<Panel>(),
            //     [LayerType.System] = new Stack<Panel>(),
            // };
        }

        
        // #region 面板视图
        //
        // public T ShowPanel<T>(string key, LayerType layer = LayerType.Back) where T: Panel
        // {
        //     var obj = ShowUI(key, layer);
        //     if (obj != null)
        //     {
        //         if (!obj.TryGetComponent<T>(out var panel))
        //         {
        //             panel = obj.AddComponent<T>();
        //             panel.OnInit();
        //
        //             if (!_panels[layer].Contains(panel)) _panels[layer].Push(panel);
        //         }
        //         
        //         panel.OnOpen();
        //
        //         return panel;
        //     }
        //
        //     return null;
        // }
        //
        // public void ShowPanel<T>(object key, Action<T> callback, LayerType layer = LayerType.Back) where T : Panel
        // {
        //     ShowUI(key, o =>
        //     {
        //         if (!o.TryGetComponent<T>(out var panel))
        //         {
        //             panel = o.AddComponent<T>();
        //             panel.OnInit();
        //             
        //             if (!_panels[layer].Contains(panel)) _panels[layer].Push(panel);
        //         }
        //         
        //         panel.OnOpen();
        //
        //         callback(panel);
        //     }, layer);
        // }
        //
        // public bool HidePanel(string key, bool release = false)
        // {
        //     var p = HideUI(key);
        //     if (p == null) return false;
        //
        //     if (p.TryGetComponent<Panel>(out var panel))
        //     {
        //         panel.OnClose();
        //         
        //         _panels[]
        //     }
        //
        //     if (release)
        //     {
        //         _cache.Remove(key);
        //
        //         // 由 Addressables 直接实例化的资源, 必须由 Addressables 释放
        //         LogUtils.InfoFormat("[UIManager] 释放UI资源: key={0}", key);
        //         var ok = AddressableManager.Instance.Release(p);
        //         LogUtils.InfoFormat("[UIManager] 释放结果: key={0}, ok={1}", key, ok);
        //
        //         // // 手工实例化的资源, 手工释放
        //         // Destroy(p);
        //
        //         return ok;
        //     }
        //
        //     return true;
        // }
        //
        // #endregion


        
        #region 通用
        
        public void ShowUI(object key, Action<GameObject> callback, LayerType layer = LayerType.Back)
        {
            if (_cache.TryGetValue(key.ToString(), out var ui))
            {
                LogUtils.InfoFormat("[UIManager] show panel from cache: layer={0}, key={1}", layer, key);
                callback?.Invoke(ui);
                return;
            }

            AddressableManager.Instance.Instantiate(key, _layers[layer], false, o =>
            {
                LogUtils.Info("[UIManager] 显示面板, 资源加载且实例化完成=" + o);
                o.name = key.ToString();

                _cache[key.ToString()] = o;

                callback?.Invoke(o);
            });
        }
        
        public GameObject ShowUI(string key, LayerType layer = LayerType.Back)
        {
            if (_cache.TryGetValue(key, out var ui))
            {
                LogUtils.InfoFormat("[UIManager] show panel from cache: layer={0}, key={1}", layer, key);
                ui.SetActive(true);
                return ui;
            }

            // var res = AddressableManager.Instance.LoadWait<GameObject>(key);
            // var obj = Instantiate(res, _layers[layer]);

            var obj = AddressableManager.Instance.InstantiateWait(key, _layers[layer]);
            obj.name = key;
            
            _cache[key] = obj;
            return obj;
        }
        
        
        public GameObject HideUI(string key)
        {
            if (_cache.TryGetValue(key, out var ui))
            {
                LogUtils.InfoFormat("[UIManager] hide ui: key={0}", key);
                ui.SetActive(false);
                return ui;
            }
       
            LogUtils.ErrorFormat("[UIManager] 隐藏UI失败, 未找到: key={0}", key);
            return null;
        }

        public bool ReleaseUI(string key)
        {
            if (_cache.TryGetValue(key, out var ui))
            {
                _cache.Remove(key);

                // 由 Addressables 直接实例化的资源, 必须由 Addressables 释放
                var ok = AddressableManager.Instance.Release(ui);
                LogUtils.InfoFormat("[UIManager] 释放UI资源: key={0}, ok={1}", key, ok);

                // // 手工实例化的资源, 手工释放
                // Destroy(p);

                return ok;
            }

            return false;
        }

        #endregion
    }
}
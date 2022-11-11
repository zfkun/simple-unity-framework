using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Suf.Resource
{
    public class AddressableManager: Base.Singleton<AddressableManager>
    {
        public override void Init()
        {
            base.Init();
            Addressables.InitializeAsync();
        }


        #region 加载操作(单个)

        public T LoadWait<T>(object key) => LoadAsync<T>(key).WaitForCompletion();

        public async Task<T> Load<T>(object key) => await LoadAsync<T>(key).Task;

        public void Load<T>(object key, Action<T> fn) => LoadAsync<T>(key).Completed += h => fn?.Invoke(h.Result);

        private AsyncOperationHandle<T> LoadAsync<T>(object key) => Addressables.LoadAssetAsync<T>(key);
        
        #endregion
        
        
        
        
        
        #region 加载操作(批量)

        public IList<T> LoadListWait<T>(IEnumerable keys) =>
            LoadListAsync<T>(keys, null, Addressables.MergeMode.Union, true).WaitForCompletion();

        public async Task<IList<T>> LoadList<T>(IEnumerable keys) =>
            await LoadListAsync<T>(keys, null, Addressables.MergeMode.Union, true).Task;

        public void LoadList<T>(IEnumerable keys, Action<IList<T>> fn) =>
            LoadListAsync<T>(keys, null, Addressables.MergeMode.Union, true).Completed += h => fn?.Invoke(h.Result);

        private AsyncOperationHandle<IList<T>> LoadListAsync<T>(IEnumerable keys, Action<T> fn,
            Addressables.MergeMode mode, bool release) => Addressables.LoadAssetsAsync(keys, fn, mode, release);

        #endregion

        
        
        
        
        
        #region 实例化操作

        /// <summary>
        /// 同步实例化资源
        /// </summary>
        /// <param name="key">资源名</param>
        /// <param name="parent">插入位置</param>
        /// <param name="isWorld">保持世界空间位置</param>
        /// <returns>AsyncOperationHandle</returns>
        public GameObject InstantiateWait(object key, Transform parent = null, bool isWorld = false)
        {
            return InstantiateAsync(key, parent, isWorld).WaitForCompletion();
        }
        
        /// <summary>
        /// 异步实例化资源
        /// </summary>
        /// <param name="key">资源名</param>
        /// <param name="parent">插入位置</param>
        /// <param name="isWorld">保持世界空间位置</param>
        /// <param name="fn">回调</param>
        public void Instantiate(object key, Transform parent = null, bool isWorld = false, Action<GameObject> fn = null)
        {
            InstantiateAsync(key, parent, isWorld).Completed += handle => fn?.Invoke(handle.Result);
        }
        
        private AsyncOperationHandle<GameObject> InstantiateAsync(object key, Transform parent = null, bool isWorld = false)
        {
            return Addressables.InstantiateAsync(key, parent, isWorld);
        }
        
        #endregion

        
        
        
        
        
        
        #region 清理操作

        public bool Release(AsyncOperationHandle handle) => Addressables.ReleaseInstance(handle);
        public bool Release(AsyncOperationHandle<GameObject> handle) => Addressables.ReleaseInstance(handle);
        public bool Release(GameObject instance) => Addressables.ReleaseInstance(instance);

        #endregion
    }
}
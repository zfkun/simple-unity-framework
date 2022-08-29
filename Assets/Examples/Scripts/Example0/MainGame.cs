using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Suf.Base;
using Suf.UI;
using Suf.Utils;

using Examples.Scripts.Example0.UI;

namespace Examples.Scripts.Example0
{
    public class MainGame: UnitySingletonAuto<MainGame>
    {
        /// <summary>
        /// 自定义指定待更新检测的 Label 集合
        /// </summary>
        private List<string> mKeys = new List<string>() { "core", "ui", "example0", "example1", "example2", "example3" };
    
        public void GameStart()
        {
            LogUtils.Info("[MainGame] GameStart");
            LogUtils.Info(Type.GetType("Examples.Scripts.Example0.UI.Example0PanelController"));
            LogUtils.Info(this.GetType().Namespace);
            PreloadGame();
            // EnterGame();
        }

        private void PreloadGame()
        {
            LogUtils.Info("[MainGame] 预加载完成");
        
            StartCoroutine(CheckUpdate());
        }

        private void EnterGame()
        {
            LogUtils.Info("[MainGame] 进入游戏");

            UIManager.Instance.ShowPanel("Example0Panel", typeof(Example0PanelController));
            // var panel = UIManager.Instance.ShowUI("Example0Panel");
            // panel.AddComponent<Example0PanelController>();
        }
        
    
        public IEnumerator CheckUpdate() {
            var isNeedUpdateCatalog = false;
            var isNeedUpdateResources = false;

            // 初始化 Addressable
            var initHandle = Addressables.InitializeAsync();
            yield return initHandle;
            LogUtils.Info("[MainGame] 开始检查更新");

            // 检查本地 Catalog 是否为最新版本
            var checkHandle = Addressables.CheckForCatalogUpdates(false);
            yield return checkHandle;
            if (checkHandle.Status == AsyncOperationStatus.Succeeded) {
                LogUtils.Info("[MainGame] 目录检查完成");
            }

            var catalogs = checkHandle.Result;
            if (catalogs != null && catalogs.Count > 0) {
                LogUtils.Info("[MainGame] 检测到 Catalogs 需要更新: " + catalogs);
                isNeedUpdateCatalog = true;
            } else {
                LogUtils.Info("[MainGame] 检测到 Catalogs 已是最新");
            }

            var sizeHandle = Addressables.GetDownloadSizeAsync(mKeys);
            if (sizeHandle.Result > 0) {
                LogUtils.Info("[MainGame] 检测到有更新资源包: " + sizeHandle.Result + " bytes");
                isNeedUpdateResources = true;
            } else {
                LogUtils.Info("[MainGame] 检测到没有资源更新");
            }

            LogUtils.Info("[MainGame] 准备进行下一步");

            if (isNeedUpdateCatalog || isNeedUpdateResources) {
                if (isNeedUpdateCatalog) {
                    yield return UpdateCatalog(catalogs);
                }
                if (isNeedUpdateResources) {
                    yield return UpdateResources();
                }
                EnterGame();
            } else {
                EnterGame();
            }
        }
    
        private IEnumerator UpdateCatalog(List<string> catalogs) {
            var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
            LogUtils.Info("[MainGame] 开始更新 Catalogs");
            while (!updateHandle.IsDone)
            {
                LogUtils.InfoFormat("[MainGame] 更新进度: {0} %", updateHandle.PercentComplete * 100);
                yield return null;
            }
            if (updateHandle.Status == AsyncOperationStatus.Succeeded) {
                LogUtils.InfoFormat("[MainGame] 更新情况：{0}", updateHandle.IsDone ? "完成" : "未完成");
            } else {
                LogUtils.ErrorFormat("[MainGame] 更新 Catalogs 失败，错误内容：{0}", updateHandle.OperationException.Message);
            }
            yield return updateHandle;

            Addressables.Release(updateHandle);
        }

        private IEnumerator UpdateResources() {
            // 清理旧资源
            var clearHandle = Addressables.ClearDependencyCacheAsync(mKeys, false);
            LogUtils.Info("[MainGame] 开始清理缓存");
            while (!clearHandle.IsDone)
            {
                LogUtils.InfoFormat("[MainGame] 清理缓存进度: {0} %", clearHandle.PercentComplete * 100);
                yield return null;
            }
            if (clearHandle.Status == AsyncOperationStatus.Succeeded) {
                LogUtils.InfoFormat("[MainGame] 清理情况：{0}", clearHandle.IsDone ? "完成" : "未完成");
            } else {
                LogUtils.ErrorFormat("[MainGame] 清理缓存失败，错误内容：{0}", clearHandle.OperationException.Message);
            }
            yield return clearHandle;
            Addressables.Release(clearHandle);

            // 下载待更新资源
            var downloadHandle = Addressables.DownloadDependenciesAsync(mKeys, Addressables.MergeMode.Union, false);
            LogUtils.Info("[MainGame] 开始更新资源");
            while (!downloadHandle.IsDone) {
                var downloadStatus = downloadHandle.GetDownloadStatus();
                LogUtils.InfoFormat("[MainGame] 下载进度: {0} %", downloadStatus.Percent * 100);
                yield return null;
            }
            if (downloadHandle.Status == AsyncOperationStatus.Succeeded) {
                LogUtils.InfoFormat("[MainGame] 下载情况：{0}", downloadHandle.IsDone ? "完成" : "未完成");
            } else {
                LogUtils.ErrorFormat("[MainGame] 下载更新包失败，错误内容：{0}", downloadHandle.OperationException.Message);
            }
            Addressables.Release(downloadHandle);
        }
    }
}
using System.Collections;
using System.Collections.Generic;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

using Suf.Base;
using Suf.Resource;
using Suf.UI;
using Suf.Utils;

public class Example1Game: UnitySingletonAuto<Example1Game>
{
    /// <summary>
    /// 自定义指定待更新检测的 Label 集合
    /// </summary>
    private List<string> mKeys = new List<string>() { "example1" };
    
    public void GameStart()
    {
        LogUtils.Info("[Example1Game] GameStart");
        // PreloadGame();
        EnterGame();
    }

    private void PreloadGame()
    {
        // var keys = new List<object>() { "example1" };
        // AddressableManager.Instance.LoadList<GameObject>(keys, objs =>
        // {
        //     LogUtils.InfoFormat("[Example1Game] 预加载完成: {0}", keys);
        //     foreach (var o in objs)
        //     {
        //         LogUtils.InfoFormat("[Example1Game] 资源: {0}", o);
        //     }
        // });

        LogUtils.Info("[MainGame] 预加载开始");
        StartCoroutine(UpdateResources());
    }
    
    private IEnumerator UpdateResources() {
        
        // // 清理旧资源
        // var clearHandle = Addressables.ClearDependencyCacheAsync(mKeys, false);
        // LogUtils.Info("[MainGame] 开始清理缓存");
        // while (!clearHandle.IsDone)
        // {
        //     LogUtils.InfoFormat("[MainGame] 清理缓存进度: {0} %", clearHandle.PercentComplete * 100);
        //     yield return null;
        // }
        // if (clearHandle.Status == AsyncOperationStatus.Succeeded) {
        //     LogUtils.InfoFormat("[MainGame] 清理情况：{0}", clearHandle.IsDone ? "完成" : "未完成");
        // } else {
        //     LogUtils.ErrorFormat("[MainGame] 清理缓存失败，错误内容：{0}", clearHandle.OperationException.Message);
        // }
        // yield return clearHandle;
        // Addressables.Release(clearHandle);

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
        
        EnterGame();
    }

    private void EnterGame()
    {
        // TODO: 载入地图UI

        // TODO: 载入地图数据

        // TODO: 载入界面UI
        PanelManager.Instance.ShowPanel<Example1PanelController>("Example1Panel");
    }
}
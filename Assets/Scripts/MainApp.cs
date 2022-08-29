using System;
using System.Collections;
using System.Collections.Generic;
using Suf.Base;
using Suf.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainApp: UnitySingletonAuto<MainApp>
{
    
    public void GameStart()
    {
        Debug.Log("[MainGame] GameStart");
        PreloadGame();
        EnterGame();
    }

    private void PreloadGame()
    {
        Debug.Log("[MainGame] 预加载完成");
    }

    private void EnterGame()
    {
        Debug.Log("[MainGame] 进入游戏");
    }

}
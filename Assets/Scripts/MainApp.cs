using Suf.Base;
using Suf.Utils;

public class MainApp: UnitySingletonAuto<MainApp>
{
    
    public void GameStart()
    {
        LogUtils.Info("[MainGame] GameStart");
        PreloadGame();
        EnterGame();
    }

    private void PreloadGame()
    {
        LogUtils.Info("[MainGame] 预加载完成");
    }

    private void EnterGame()
    {
        LogUtils.Info("[MainGame] 进入游戏");
    }

}
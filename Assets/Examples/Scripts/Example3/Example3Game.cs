using Suf.Base;
using Suf.UI;
using Suf.Utils;

public class Example3Game : UnitySingletonAuto<Example3Game>
{
    public void GameStart()
    {
        LogUtils.Info("[Example3Game] GameStart");
        EnterGame();
    }

    private void EnterGame()
    {
        PanelManager.Instance.ShowPanel<Example3PanelController>("Example3Panel");
    }
}

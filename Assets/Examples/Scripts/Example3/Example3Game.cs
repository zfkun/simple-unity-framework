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
        UIManager.Instance.ShowPanel("Example3Panel", typeof(Example3PanelController));
    }
}

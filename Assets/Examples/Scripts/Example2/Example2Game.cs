using Suf.Base;
using Suf.UI;
using Suf.Utils;

public class Example2Game : UnitySingletonAuto<Example2Game>
{
    public void GameStart()
    {
        LogUtils.InfoFormat("[Example2Game] GameStart");
        EnterGame();
    }

    private void EnterGame()
    {
        UIManager.Instance.ShowPanel("Example2Panel", typeof(Example2PanelController));
    }
}
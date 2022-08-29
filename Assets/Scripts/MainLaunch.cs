using Suf.Base;
using Suf.UI;

public class MainLaunch: UnitySingletonAuto<MainLaunch>
{
    private void Start()
    {
        // init framework
        UIManager.Instance.Init();

        
        // check update
        

        // start game
        MainApp.Instance.GameStart();
    }
}
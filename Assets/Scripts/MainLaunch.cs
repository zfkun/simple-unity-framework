using Suf.Application;
using Suf.Base;
using Suf.Resource;
using Suf.UI;

public class MainLaunch: UnitySingletonAuto<MainLaunch>
{
    private void Start()
    {
        // init framework
        ApplicationManager.Instance.Init();
        AddressableManager.Instance.Init();
        UIManager.Instance.Init();

        
        // check update
        

        // start game
        MainApp.Instance.GameStart();
    }
}
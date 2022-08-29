using Suf.Application;
using Suf.Base;
using Suf.Resource;
using Suf.UI;

public class Example3Launch : UnitySingletonAuto<Example3Launch>
{
    private void Start()
    {
        // init framework
        ApplicationManager.Instance.Init();
        AddressableManager.Instance.Init();
        UIManager.Instance.Init();

        // check update

        // start game
        Example3Game.Instance.GameStart();
    }
}

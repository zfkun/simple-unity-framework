using Suf.Application;
using Suf.Base;
using Suf.Resource;
using Suf.UI;

public class Example2Launch : UnitySingletonAuto<Example2Launch>
{
    private void Start()
    {
        // init framework
        ApplicationManager.Instance.Init();
        AddressableManager.Instance.Init();
        UIManager.Instance.Init();

        // check update

        // start game
        Example2Game.Instance.GameStart();
    }
}

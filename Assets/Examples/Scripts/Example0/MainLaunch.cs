using System.Resources;
using Suf.Application;
using Suf.Base;
using Suf.Resource;
using Suf.UI;

namespace Examples.Scripts.Example0
{
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
            MainGame.Instance.GameStart();
        }
    }
}
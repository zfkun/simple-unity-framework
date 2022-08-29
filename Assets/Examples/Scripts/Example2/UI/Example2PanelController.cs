using Suf.UI;
using Suf.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Example2PanelController: Panel
{
    private MyTestFactorySO _myFactory;
    private MyTestPoolSO _myPool;
    private Transform _gridGroup;

    private void Start()
    {
        _myFactory = ScriptableObject.CreateInstance<MyTestFactorySO>();
        _myFactory.name = "MyTestFactory";

        _myPool = ScriptableObject.CreateInstance<MyTestPoolSO>();
        _myPool.name = "MyTestPool";
        _myPool.Factory = _myFactory;

        // 展示区
        _gridGroup = transform.Find("GridGroup");
        
        // 按钮区
        transform.Find("BtnGroup/PrewarmBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnPrewarmBtnClick);
        transform.Find("BtnGroup/RequestBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnRequestBtnClick);
        transform.Find("BtnGroup/ReturnBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnReturnBtnClick);
    }

    private void OnPrewarmBtnClick()
    {
        _myPool.Prewarm(10);
    }
    
    private void OnRequestBtnClick()
    {
        GameObjectUtils.SetParent(_myPool.Request(), _gridGroup.gameObject);
    }
    
    private void OnReturnBtnClick()
    {
        if (_gridGroup.childCount > 0)
        {
            _myPool.Return(_gridGroup.GetChild(0).gameObject);
        }
    }
}
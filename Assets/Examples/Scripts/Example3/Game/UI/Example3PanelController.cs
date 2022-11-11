using Suf.Pool;
using Suf.UI;
using Suf.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Example3PanelController: Panel
{
    public new static readonly UIData data = new UIData("Example3Panel", LayerType.Middle);
    
    private Transform _gridGroupA;
    private Transform _gridGroupB;

    private void Start()
    {
        // 展示区
        _gridGroupA = transform.Find("GridGroupA");
        _gridGroupB = transform.Find("GridGroupB");
        
        // 按钮区
        transform.Find("BtnGroupA/PrewarmBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnPrewarmBtnClickA);
        transform.Find("BtnGroupA/RequestBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnRequestBtnClickA);
        transform.Find("BtnGroupA/ReturnBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnReturnBtnClickA);
        transform.Find("BtnGroupB/PrewarmBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnPrewarmBtnClickB);
        transform.Find("BtnGroupB/RequestBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnRequestBtnClickB);
        transform.Find("BtnGroupB/ReturnBtn")?.GetComponent<Button>()?.onClick?.AddListener(OnReturnBtnClickB);
    }

    #region A

    private void OnPrewarmBtnClickA()
    {
        PoolManager.Instance.Prewarm("AvatarA", 10);
    }
    
    private void OnRequestBtnClickA()
    {
        var obj = PoolManager.Instance.Request("AvatarA");
        GameObjectUtils.SetParent(obj, _gridGroupA.gameObject);
    }
    
    private void OnReturnBtnClickA()
    {
        if (_gridGroupA.childCount > 0)
        {
            PoolManager.Instance.Return("AvatarA", _gridGroupA.GetChild(0).gameObject);
        }
    }
    
    #endregion
    
    #region B

    private void OnPrewarmBtnClickB()
    {
        PoolManager.Instance.Prewarm("AvatarB", 10);
    }
    
    private void OnRequestBtnClickB()
    {
        var obj = PoolManager.Instance.Request("AvatarB");
        GameObjectUtils.SetParent(obj, _gridGroupB.gameObject);
    }
    
    private void OnReturnBtnClickB()
    {
        if (_gridGroupB.childCount > 0)
        {
            PoolManager.Instance.Return("AvatarB", _gridGroupB.GetChild(0).gameObject);
        }
    }
    
    #endregion
}
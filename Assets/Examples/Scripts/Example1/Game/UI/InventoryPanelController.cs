using Suf.UI;
using UnityEngine.UI;

public class InventoryPanelController: Panel
{
    private Button _closeBtn;

    private void Start()
    {
        _closeBtn = transform.Find("Bg/Close")?.GetComponent<Button>();
        if (_closeBtn) _closeBtn.onClick.AddListener(OnCloseClick);
    }

    private void OnDestroy()
    {
        if (_closeBtn)
        {
            _closeBtn.onClick.RemoveListener(OnCloseClick);
            _closeBtn = null;
        }
    }

    private void OnCloseClick()
    {
        if (_closeBtn)
        {
            _closeBtn.onClick.RemoveListener(OnCloseClick);
            _closeBtn = null;
            
            PanelManager.Instance.HidePanel("InventoryPanel", true);
        }
    }
}
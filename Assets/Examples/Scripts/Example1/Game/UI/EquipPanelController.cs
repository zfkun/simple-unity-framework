using Suf.UI;
using UnityEngine.UI;

public class EquipPanelController : Panel
{
    private Button m_CloseBtn;

    private void Start()
    {
        m_CloseBtn = transform.Find("Bg/Close")?.GetComponent<Button>();
        if (m_CloseBtn) m_CloseBtn.onClick.AddListener(OnCloseClick);
    }

    private void OnDestroy()
    {
        if (m_CloseBtn) m_CloseBtn.onClick.RemoveListener(OnCloseClick);
    }

    private void OnCloseClick()
    {
        if (m_CloseBtn)
        {
            m_CloseBtn.onClick.RemoveListener(OnCloseClick);
            m_CloseBtn = null;
            
            PanelManager.Instance.HidePanel("EquipPanel");
        }
    }
}
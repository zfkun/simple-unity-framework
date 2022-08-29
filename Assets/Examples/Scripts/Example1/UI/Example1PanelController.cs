using Suf.Resource;
using Suf.UI;
using Suf.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Example1PanelController: Panel
{
    private Panel _equipPanel;
    private Panel _inventoryPanel;

    private void Start()
    {
        // 按钮区
        transform.Find("BtnBar/Equip")?.GetComponent<Button>()?.onClick?.AddListener(ShowEquip);
        transform.Find("BtnBar/Item")?.GetComponent<Button>()?.onClick?.AddListener(ShowItem);
    }
    
    private void ShowEquip()
    {
        if (_equipPanel == null)
        {
            _equipPanel = UIManager.Instance.ShowPanel("EquipPanel", typeof(EquipPanelController), LayerType.Middle);
        }
        else
        {
            UIManager.Instance.HidePanel("EquipPanel");
            // Destroy(_equipPanel.gameObject);
            _equipPanel = null;
        }
    }
    
    private void ShowItem()
    {
        if (_inventoryPanel == null)
        {
            _inventoryPanel = UIManager.Instance.ShowPanel("InventoryPanel", typeof(InventoryPanelController), LayerType.Middle);
        }
        else
        {
            UIManager.Instance.HidePanel("InventoryPanel");
            // Destroy(_inventoryPanel.gameObject);
            _inventoryPanel = null;
        }
    }
}
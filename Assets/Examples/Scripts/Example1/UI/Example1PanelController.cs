using Suf.UI;
using UnityEngine.UI;

public class Example1PanelController: Panel
{
    private EquipPanelController _equipPanel;
    private InventoryPanelController _inventoryPanel;

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
            _equipPanel = PanelManager.Instance.ShowPanel<EquipPanelController>("EquipPanel", LayerType.Middle);
        }
        else
        {
            PanelManager.Instance.HidePanel("EquipPanel", true);
            // Destroy(_equipPanel.gameObject);
            _equipPanel = null;
        }
    }
    
    private void ShowItem()
    {
        if (_inventoryPanel == null)
        {
            _inventoryPanel = PanelManager.Instance.ShowPanel<InventoryPanelController>("InventoryPanel", LayerType.Middle);
        }
        else
        {
            PanelManager.Instance.HidePanel("InventoryPanel");
            // Destroy(_inventoryPanel.gameObject);
            _inventoryPanel = null;
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class LHWInventoryUIController : MonoBehaviour
{
    public LHWDynamicInventoryDisplay InventoryPanel;

    private void Awake()
    {
        InventoryPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Inventory.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        Inventory.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    void Update()
    {
        if (!InventoryPanel.gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame) DisplayInventory(new InventorySystem(20));

        else if(InventoryPanel.gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame)
            InventoryPanel.gameObject.SetActive(false);
    }
    public void DisplayInventory(InventorySystem invToDisplay)
    {
        InventoryPanel.gameObject.SetActive(true);
        InventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class LHWInventoryUIController : MonoBehaviour
{
    public LHWDynamicInventoryDisplay ChestPanel;
    public LHWDynamicInventoryDisplay PlayerBackpackPanel;

    private void Awake()
    {
        ChestPanel.gameObject.SetActive(false);
        PlayerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Inventory.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }

    private void OnDisable()
    {
        Inventory.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    void Update()
    {
         if(ChestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            ChestPanel.gameObject.SetActive(false);

        if (PlayerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            PlayerBackpackPanel.gameObject.SetActive(false);
    }
    private void DisplayInventory(InventorySystem chestInvToDisplay)
    {
        ChestPanel.gameObject.SetActive(true);
        ChestPanel.RefreshDynamicInventory(chestInvToDisplay);
    }

    private void DisplayPlayerBackpack(InventorySystem playerInvToDisplay)
    {
        PlayerBackpackPanel.gameObject.SetActive(true);
        PlayerBackpackPanel.RefreshDynamicInventory(playerInvToDisplay);
    }
}

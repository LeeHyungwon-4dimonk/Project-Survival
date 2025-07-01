using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Controls Dynamic Inventory UIs.
/// </summary>
public class LHWInventoryUIController : MonoBehaviour
{
    public LHWDynamicInventoryDisplay ChestPanel;
    public LHWDynamicInventoryDisplay PlayerBackpackPanel;
    public LHWDynamicInventoryDisplay PlayerCraftingPanel;

    private void Awake()
    {
        ChestPanel.gameObject.SetActive(false);
        PlayerBackpackPanel.gameObject.SetActive(false);
        PlayerCraftingPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Inventory.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
        PlayerInventoryHolder.OnPlayerCraftingDisplayRequested += DisplayPlayerCraftingPanel;
    }

    private void OnDisable()
    {
        Inventory.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
        PlayerInventoryHolder.OnPlayerCraftingDisplayRequested -= DisplayPlayerCraftingPanel;
    }

    // I've set esc key to exit temporary. Can change inactivate key.
    void Update()
    {
        if (ChestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            ChestPanel.gameObject.SetActive(false);

        if (PlayerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            PlayerBackpackPanel.gameObject.SetActive(false);

        if(PlayerCraftingPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            PlayerCraftingPanel.gameObject.SetActive(false) ;
    }

    /// <summary>
    /// Display Dynamic Inventory.(Chest)
    /// </summary>
    /// <param name="chestInvToDisplay"></param>
    private void DisplayInventory(InventorySystem chestInvToDisplay)
    {
        ChestPanel.gameObject.SetActive(true);
        ChestPanel.RefreshDynamicInventory(chestInvToDisplay);
    }

    /// <summary>
    /// Display Player Backpack Inventory.
    /// </summary>
    /// <param name="playerInvToDisplay"></param>
    private void DisplayPlayerBackpack(InventorySystem playerInvToDisplay)
    {
        PlayerBackpackPanel.gameObject.SetActive(true);
        PlayerBackpackPanel.RefreshDynamicInventory(playerInvToDisplay);
    }

    private void DisplayPlayerCraftingPanel(InventorySystem playerInvToDisplay)
    {
        PlayerCraftingPanel.gameObject.SetActive(true);
        PlayerCraftingPanel.RefreshDynamicInventory(playerInvToDisplay);
    }
}
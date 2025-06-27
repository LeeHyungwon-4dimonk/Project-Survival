using UnityEngine;

/// <summary>
/// Crafting Inventory.
/// </summary>
public class CraftingInventoryHolder : Inventory, IInteractable
{
    /// <summary>
    /// Activate Interaction UI
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
        return "Press E Key to Interact";
    }

    /// <summary>
    /// Get Key.
    /// </summary>
    /// <returns></returns>
    public KeyCode GetKey()
    {
        return KeyCode.E;
    }

    /// <summary>
    /// Activate Crafting Inventory UI.
    /// </summary>
    public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(_inventorySystem);
    }
}

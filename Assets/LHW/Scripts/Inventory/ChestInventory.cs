using UnityEngine;

/// <summary>
/// Chest Inventory.
/// Interaction method is not complete, so test not conducted.
/// </summary>
public class ChestInventory : Inventory, IInteractable
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
    /// Activate Chest Inventory UI.
    /// </summary>
    public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(_inventorySystem);
        Debug.Log("상호작용");
    }
}
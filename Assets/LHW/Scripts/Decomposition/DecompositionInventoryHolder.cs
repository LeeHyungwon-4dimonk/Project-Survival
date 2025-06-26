using UnityEngine;

/// <summary>
/// Crafting Decomposition Inventory.
/// </summary>
public class DecompositionInventoryHolder : Inventory, IInteractable
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
    /// Activate Decomposition Inventory UI.
    /// </summary>
    public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(_inventorySystem);
    }
}

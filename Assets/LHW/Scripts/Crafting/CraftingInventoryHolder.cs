using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Crafting Inventory.
/// </summary>
public class CraftingInventoryHolder : MonoBehaviour, IInteractable
{
    [SerializeField] public Image CraftingPanel;

    protected void Awake()
    {
        CraftingPanel.gameObject.SetActive(false);
    }

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
        CraftingPanel.gameObject.SetActive(true);
    }
}
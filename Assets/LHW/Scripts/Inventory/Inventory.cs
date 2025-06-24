using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] protected InventorySystem _inventorySystem;

    public InventorySystem InventorySystem => _inventorySystem;

    private void Awake() => Init();

    /// <summary>
    /// Construct inventory.
    /// </summary>
    private void Init()
    {
        _inventorySystem = new InventorySystem(_inventorySize);
    }
}
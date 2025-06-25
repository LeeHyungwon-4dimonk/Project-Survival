using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PopUp Inventory that player can hold.(Backpack Inventory)
/// </summary>
public class PlayerInventoryHolder : Inventory
{
    [SerializeField] protected int _playerInventorySize;
    [SerializeField] protected InventorySystem _playerInventorySystem;

    public InventorySystem PlayerInventorySystem => _playerInventorySystem;

    public static Action<InventorySystem> OnPlayerBackpackDisplayRequested;

    protected override void Awake()
    {
        base.Awake();

        _playerInventorySystem = new InventorySystem(_playerInventorySize);
    }

    private void Update()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame)
            OnPlayerBackpackDisplayRequested?.Invoke(_playerInventorySystem);
    }

    /// <summary>
    /// Add item to inventory.
    /// Item is add primary to player hot bar, and into the backpack.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool AddItem(LHWTestItem data, int amount)
    {
        if (_inventorySystem.AddItem(data, amount))
        {
            return true;
        }
        else if(_playerInventorySystem.AddItem(data, amount))
        {
            return true;
        }

        return false;
    }
}

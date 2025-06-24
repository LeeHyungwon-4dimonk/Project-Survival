using System.Collections.Generic;
using UnityEngine;

public class LHWStaticInventoryDisplay : LHWInventoryDisplay
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private LHWInventorySlot_UI[] _slots;

    protected override void Start()
    {
        base.Start();

        if (_inventory != null)
        {
            _inventorySystem = _inventory.InventorySystem;
            _inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");

        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        _slotDictionary = new Dictionary<LHWInventorySlot_UI, InventorySlots>();

        if(_slots.Length != _inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject.name}");

        for(int i = 0; i < _inventorySystem.InventorySize; i++)
        {
            _slotDictionary.Add(_slots[i], _inventorySystem.InventorySlots[i]);
            _slots[i].Init(_inventorySystem.InventorySlots[i]);
        }
    }
}

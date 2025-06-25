using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Inventory UI Display, that is not permanently activated in scene.
/// </summary>
public class LHWDynamicInventoryDisplay : LHWInventoryDisplay
{
    [SerializeField] protected LHWInventorySlot_UI _slotPrefab;
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Update the UI when display is activated.
    /// </summary>
    /// <param name="invToDisplay"></param>
    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        _inventorySystem = invToDisplay;
        if(_inventorySystem != null) _inventorySystem.OnInventorySlotChanged += UpdateSlot;
        AssignSlot(invToDisplay);
    }

    /// <summary>
    /// Print out the current status of inventory.
    /// </summary>
    /// <param name="invToDisplay"></param>
    // It need to be applied Object Pool Patterns?
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        _slotDictionary = new Dictionary<LHWInventorySlot_UI, InventorySlots>();

        if (invToDisplay == null) return;

        for(int i = 0; i < invToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(_slotPrefab, transform);
            _slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    /// <summary>
    /// Clear the inventory.
    /// </summary>
    // It need to be applied Object Pool Patterns?
    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if(_slotDictionary != null) _slotDictionary.Clear();
    }

    private void OnDisable()
    {
        if (_inventorySystem != null) _inventorySystem.OnInventorySlotChanged -= UpdateSlot;
    }
}

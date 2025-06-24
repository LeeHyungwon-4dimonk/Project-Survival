using System.Collections.Generic;
using UnityEngine;

public abstract class LHWInventoryDisplay : MonoBehaviour
{
    [SerializeField] LHWMouseItemData _mouseInventoryItem;

    protected InventorySystem _inventorySystem;
    protected Dictionary<LHWInventorySlot_UI, InventorySlots> _slotDictionary;

    public InventorySystem InventorySystem => _inventorySystem;
    public Dictionary<LHWInventorySlot_UI, InventorySlots> SlotDitionary => _slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlots updatedSlot)
    {
        foreach (var slot in SlotDitionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(LHWInventorySlot_UI clickedUISlot)
    {
        if (clickedUISlot.AssignedInventorySlot.Data != null && _mouseInventoryItem.AssignedInventorySlot.Data == null)
        {
            _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        if (clickedUISlot.AssignedInventorySlot.Data == null && _mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.ClearSlot();
        }
    }
}

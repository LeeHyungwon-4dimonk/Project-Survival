using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class LHWInventoryDisplay : MonoBehaviour
{
    [SerializeField] LHWMouseItemData _mouseInventoryItem;

    protected InventorySystem _inventorySystem;
    protected Dictionary<LHWInventorySlot_UI, InventorySlots> _slotDictionary;

    public InventorySystem InventorySystem => _inventorySystem;
    public Dictionary<LHWInventorySlot_UI, InventorySlots> SlotDitionary => _slotDictionary;

    protected virtual void Start() { }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    /// <summary>
    /// Updates the data to UI.
    /// </summary>
    /// <param name="updatedSlot"></param>
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

    /// <summary>
    /// If the slot is clicked.
    /// </summary>
    /// <param name="clickedUISlot"></param>
    public void SlotClicked(LHWInventorySlot_UI clickedUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

        // If there's item in the inventory slot, and not in mouse slot.
        if (clickedUISlot.AssignedInventorySlot.Data != null && _mouseInventoryItem.AssignedInventorySlot.Data == null)
        {
            // If the item is over two stack and player is holding shift key? Split the stack.
            // Change isShiftPressed if you want to change splitKey.
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlots halfStackSlot))
            {
                _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            // If the item is not stackable or one stack, just move the item to mouse slot.
            else
            {
                _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        // If there's no item in the inventory slot, and in mouse slot.
        if (clickedUISlot.AssignedInventorySlot.Data == null && _mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.ClearSlot();
            return;
        }

        // If there item in the inventory slot and in the mouse slot.
        if (clickedUISlot.AssignedInventorySlot.Data != null && _mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.Data == _mouseInventoryItem.AssignedInventorySlot.Data;

            // If inventory item is same as mousehold item. And the stack remain in inventory is enough.
            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(_mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                _mouseInventoryItem.ClearSlot();
                return;
            }
            // If inventory item is same as mousehold item. And the stack remain in inventory is not enough.
            else if (isSameItem &&
                !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(_mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                // stack is full so swap the items
                if (leftInStack < 1)
                {
                    SwapSlots(clickedUISlot);
                    return;
                }
                // slot is not at max, so take what's need from the mouse inventory
                else
                {
                    int remainingOnMouse = _mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlots(_mouseInventoryItem.AssignedInventorySlot.Data, remainingOnMouse);
                    _mouseInventoryItem.ClearSlot();
                    _mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            // If the inventory item is different with mousehold item.
            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

    /// <summary>
    /// Swap items in inventory with in mouse slot.
    /// </summary>
    /// <param name="clickedUISlot"></param>
    private void SwapSlots(LHWInventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlots(_mouseInventoryItem.AssignedInventorySlot.Data, _mouseInventoryItem.AssignedInventorySlot.StackSize);
        _mouseInventoryItem.ClearSlot();

        _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
        
        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
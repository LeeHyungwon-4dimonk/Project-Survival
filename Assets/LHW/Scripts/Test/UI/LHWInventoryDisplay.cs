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
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

        if (clickedUISlot.AssignedInventorySlot.Data != null && _mouseInventoryItem.AssignedInventorySlot.Data == null)
        {
            // If player is holding shift key? Split the stack. change isShiftPressed if you want to change splitKey
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlots halfStackSlot))
            {
                _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        if (clickedUISlot.AssignedInventorySlot.Data == null && _mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.ClearSlot();
            return;
        }

        if (clickedUISlot.AssignedInventorySlot.Data != null && _mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.Data == _mouseInventoryItem.AssignedInventorySlot.Data;

            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(_mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                _mouseInventoryItem.ClearSlot();
                return;
            }
            else if(isSameItem &&
                !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(_mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                // stack is full so swap the items
                if (leftInStack < 1)
                {
                    SwapSlots(clickedUISlot);
                    return;
                }
                // slot is not at max, so take what's need form the mouse inventory
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
            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

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

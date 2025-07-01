using UnityEngine;

[System.Serializable]
public class InventorySlots
{
    [SerializeField] private LHWTestItem _data;
    [SerializeField] private int _stackSize;

    public LHWTestItem Data => _data;
    public int StackSize => _stackSize;

    /// <summary>
    /// Constructor to make occupied invetory slot 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="amount"></param>
    public InventorySlots(LHWTestItem source, int amount)
    {
        _data = source;
        _stackSize = amount;
    }

    /// <summary>
    /// constructor to make an empty inventory slot.
    /// </summary>
    public InventorySlots()
    {
        ClearSlot();
    }

    /// <summary>
    /// Clears the slot
    /// </summary>
    public void ClearSlot()
    {
        _data = null;
        _stackSize = -1;
    }

    /// <summary>
    /// Assigns an item to the slot
    /// </summary>
    /// <param name="invSlot"></param>
    public void AssignItem(InventorySlots invSlot)
    {
        // Does the slot contains the same item? Add to stack if so.
        if(_data == invSlot.Data) AddToStack(invSlot._stackSize);
        // Overwrite slot with the inventory slot that we;re passing in.
        else
        {
            _data = invSlot.Data;
            _stackSize = 0;
            AddToStack(invSlot._stackSize);
        }
    }

    /// <summary>
    /// Updates slot directly
    /// </summary>
    /// <param name="source"></param>
    /// <param name="amount"></param>
    public void UpdateInvetorySlots(LHWTestItem source, int amount)
    {
        _data = source;
        _stackSize = amount <= _data.MaxStackSize ? amount : _data.MaxStackSize;
    }

    /// <summary>
    /// Defines whether there's enough room in the stack,
    /// and return the remaining stack.
    /// </summary>
    /// <param name="amountToAdd"></param>
    /// <param name="amountRemaining"></param>
    /// <returns></returns>
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = _data.MaxStackSize - _stackSize;
        return RoomLeftInStack(amountToAdd);
    }
    /// <summary>
    /// Defines whether there's enough room in the stack.
    /// </summary>
    /// <param name="amountToAdd"></param>
    /// <returns></returns>
    public bool RoomLeftInStack(int amountToAdd)
    {
        if (_stackSize + amountToAdd <= _data.MaxStackSize) return true;
        else return false;
    }

    /// <summary>
    /// Add items in stack
    /// </summary>
    /// <param name="amount"></param>
    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    /// <summary>
    /// Remove items in stack
    /// </summary>
    /// <param name="amount"></param>
    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }

    /// <summary>
    /// Defines whether items can be split,
    /// and return split stack size.
    /// </summary>
    /// <param name="splitStack"></param>
    /// <returns></returns>
    public bool SplitStack(out InventorySlots splitStack)
    {
        if(_stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(_stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlots(_data, halfStack);
        return true;
    }
}
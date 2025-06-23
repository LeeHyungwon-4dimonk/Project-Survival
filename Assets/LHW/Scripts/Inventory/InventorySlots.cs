using UnityEngine;

[System.Serializable]
public class InventorySlots
{
    [SerializeField] private LHWTestItem _data;
    [SerializeField] private int _stackSize;

    public LHWTestItem Data => _data;
    public int StackSize => _stackSize;

    public InventorySlots(LHWTestItem source, int amount)
    {
        _data = source;
        _stackSize = amount;
    }

    public InventorySlots()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _data = null;
        _stackSize = -1;
    }

    public void UpdateInvetorySlots(LHWTestItem source, int amount)
    {
        _data = source;
        _stackSize = amount <= _data.MaxStackSize ? amount : _data.MaxStackSize;

    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = _data.MaxStackSize - _stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (_stackSize + amountToAdd <= _data.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }
    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }
}

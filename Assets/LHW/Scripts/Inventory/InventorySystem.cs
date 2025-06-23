using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    [SerializeField] private List<InventorySlots> _inventorySlots;

    public List<InventorySlots> InventorySlots => _inventorySlots;

    public int InventorySize => InventorySlots.Count;
    public Action<InventorySlots> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlots>(size);

        for(int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlots());
        }
    }

    public bool AddItem(LHWTestItem item, int amountToAdd)
    {
        int lastIndex = -1;
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (_inventorySlots[i].Data == null)
            {
                _inventorySlots[i].UpdateInvetorySlots(item, amountToAdd);
            }

            if (_inventorySlots[i].Data == item && _inventorySlots[i].RoomLeftInStack(amountToAdd))
            {
                _inventorySlots[i].AddToStack(amountToAdd);
                lastIndex = i;
                OnInventorySlotChanged?.Invoke(_inventorySlots[i]);
                break;
            }
            // need to be fixed(bug)
            else if (_inventorySlots[i].Data == item)
            {
                _inventorySlots[i].RoomLeftInStack(amountToAdd, out int amountRemaining);
                if (amountRemaining > 0)
                {
                    _inventorySlots[i].AddToStack(amountRemaining);
                    amountToAdd -= amountRemaining;
                    OnInventorySlotChanged?.Invoke(_inventorySlots[i]);
                    Debug.Log("This item Stack is Full");
                    continue;
                }
            }            
        }

        if (lastIndex == -1)
        {
            Debug.Log("Inventory is Full.");
            return false;
        }

        Debug.Log("Item is Added.");
        return true;
    }
}

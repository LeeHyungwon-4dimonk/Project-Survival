using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlots> _inventorySlots;

    public List<InventorySlots> InventorySlots => _inventorySlots;

    public int InventorySize => InventorySlots.Count;
    public Action<InventorySlots> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlots>(size);

        for (int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlots());
        }
    }

    /// <summary>
    /// Add singular Item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(LHWTestItem item)
    {
        return AddItem(item, 1);
    }

    /// <summary>
    /// Add item with amount
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amountToAdd"></param>
    /// <returns></returns>
    public bool AddItem(LHWTestItem item, int amountToAdd)
    {
        // if there's item stack already, try adding amount on the stack
        if (AlreadyHasItemStack(item, out List<InventorySlots> inventorySlots))
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
                else
                {
                    slot.RoomLeftInStack(amountToAdd, out int amountRemaining);
                    if (amountRemaining > 0)
                    {
                        slot.AddToStack(amountRemaining);
                        OnInventorySlotChanged?.Invoke(slot);
                        amountToAdd -= amountRemaining;
                    }
                }
            }
        }

        // if there's no item stack, then try adding amount on the new stack
        while (amountToAdd > 0)
        {
            if (HasEmptyStack(out InventorySlots emptySlot))
            {
                if (amountToAdd <= item.MaxStackSize)
                {
                    emptySlot.UpdateInvetorySlots(item, amountToAdd);
                    OnInventorySlotChanged?.Invoke(emptySlot);
                    return true;
                }
                else
                {
                    emptySlot.UpdateInvetorySlots(item, item.MaxStackSize);
                    amountToAdd -= item.MaxStackSize;
                }
            }
            else
            {
                Debug.Log("Inventory is full.1");
                return false;
            }
        }

        Debug.Log("Inventory is full.2");
        return false;
    }

    public bool AlreadyHasItemStack(LHWTestItem item, out List<InventorySlots> inventorySlots)
    {
        inventorySlots = InventorySlots.Where(i => i.Data == item).ToList();
        return inventorySlots != null && inventorySlots.Count > 0;
    }

    public bool HasEmptyStack(out InventorySlots emptySlot)
    {
        emptySlot = InventorySlots.FirstOrDefault(i => i.Data == null);
        return emptySlot != null;
    }
}

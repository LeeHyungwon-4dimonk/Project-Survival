using System;
using UnityEngine;

public class DecompositionSystem : MonoBehaviour
{
    [SerializeField] private ItemSO[] _decompositionItem;
    [SerializeField] private int[] _decompositionStack;

    public event Action OnDecompositionSlotUpdated;

    /// <summary>
    /// Read Data of decomposition slot.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    public ItemSO ReadFromDecompositionSlot(int index, out int stack)
    {
        stack = _decompositionStack[index];
        return _decompositionItem[index];
    }

    /// <summary>
    /// Used as OnClick event.
    /// Add all decomposible item into slots.
    /// </summary>
    public void AddAllDecomposibleItemIntoSlot()
    {
        int inventoryLength = InventoryManager.Instance.InventoryCount;
        for(int i = 0; i < inventoryLength; i++)
        {
            ItemSO item = InventoryManager.Instance.ReadFromInventory(i, out int stack);

            if(item!= null && item.IsDecomposable)
            {
                InventoryManager.Instance.SendItemToDecomposition(i);
            }
        }
    }

    /// <summary>
    /// Used as OnClick event.
    /// Return all items from decomposition slot to inventory.
    /// </summary>
    public void ReturnAllDecomposibleItemIntoSlot()
    {
        for(int i = 0; i < _decompositionItem.Length; i++)
        {
            InventoryManager.Instance.ReturnItemFromDecomposition(i);
        }
    }

    /// <summary>
    /// Used as OnClick event.
    /// Decompose all items in the slots and return energy gain.
    /// </summary>
    public void DecomposeAllItems()
    {
        int energy = 0;
        for(int i = 0; i < _decompositionItem.Length;i++)
        {
            if(_decompositionItem[i] != null) energy += _decompositionItem[i].Energy * _decompositionStack[i];
            _decompositionItem[i] = null;
            _decompositionStack[i] = 0;
        }
        OnDecompositionSlotUpdated?.Invoke();

        Debug.Log(energy);
        // TODO : energy gain.
    }

    /// <summary>
    /// Add item to Decomposition Slot.(From Inventory)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    public bool AddItemToDecompositionSlot(ItemSO item, int stack)
    {
        if (!item.IsDecomposable) return false;

        int remain = stack;
        while (remain > 0)
        {
            for (int i = 0; i < _decompositionItem.Length; i++)
            {
                if (_decompositionItem[i] == item)
                {
                    remain = DecompositionSlotTryAdd(item, i, remain);
                    if (remain <= 0) break;
                }
            }

            if (remain <= 0) break;

            for (int i = 0; i < _decompositionItem.Length; i++)
            {
                if (_decompositionItem[i] == null)
                {
                    remain = DecompositionSlotTryAdd(item, i, remain);
                    if (remain <= 0) break;
                }
            }

            if (remain <= 0) break;

            else
            {
                Debug.Log("slot is full"); return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Return Item to the Inventory.
    /// </summary>
    /// <param name="index"></param>
    public void ReturnItemToInventory(int index)
    {
        if(index == -1) return; 

        if (_decompositionItem[index] != null)
        {
            InventoryManager.Instance.AddItemToInventory(_decompositionItem[index], _decompositionStack[index]);

            _decompositionItem[index] = null;
            _decompositionStack[index] = 0;
            OnDecompositionSlotUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Move item in the decomposition slot.
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    public void MoveItemInDecompositionSlot(int startIndex, int endIndex)
    {
        // if there is no item in start cell.
        if (startIndex == -1 || endIndex == -1)
        {
            return;
        }

        // if there is item in start cell and drag into empty cell.
        else if (_decompositionItem[startIndex] != null && endIndex != -1 && _decompositionItem[endIndex] == null)
        {
            DecompositionSlotTryAdd(_decompositionItem[startIndex], endIndex, _decompositionStack[startIndex]);
            _decompositionItem[startIndex] = null;
            _decompositionStack[startIndex] = 0;
            OnDecompositionSlotUpdated?.Invoke();
        }

        // if there is item both in start and end
        else if (_decompositionItem[startIndex] != null && _decompositionItem[endIndex] != null)
        {
            // if item is same and stack is same, return.
            if (_decompositionItem[startIndex] == _decompositionItem[endIndex] && _decompositionStack[startIndex] == _decompositionStack[endIndex]) return;

            ItemSO tempItem = _decompositionItem[endIndex];
            int tempStack = _decompositionStack[endIndex];

            _decompositionItem[endIndex] = _decompositionItem[startIndex];
            _decompositionStack[endIndex] = _decompositionStack[startIndex];

            _decompositionItem[startIndex] = tempItem;
            _decompositionStack[startIndex] = tempStack;
            OnDecompositionSlotUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Try add item in the decomposition slot.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    private int DecompositionSlotTryAdd(ItemSO item, int index, int amount)
    {
        _decompositionItem[index] = item;
        // If the stack is enough.
        if (amount <= (item.MaxStackSize - _decompositionStack[index]))
        {
            _decompositionStack[index] += amount;
            OnDecompositionSlotUpdated?.Invoke();
            return 0;
        }
        // If the stack is not enough and has space.
        else if (item.MaxStackSize > _decompositionStack[index])
        {
            amount -= (item.MaxStackSize - _decompositionStack[index]);
            _decompositionStack[index] = item.MaxStackSize;            
            OnDecompositionSlotUpdated?.Invoke();
            return amount;
        }
        // If the stack is full.
        else
        {
            return amount;
        }
    }
}
using System;
using UnityEngine;

public class DecompositionSystem : MonoBehaviour
{
    [SerializeField] private ItemSO[] _decompositionItem;
    [SerializeField] private int[] _decompositionStack;

    public event Action OnDecompositionSlotUpdated;

    public ItemSO ReadFromDecompositionSlot(int index, out int stack)
    {
        stack = _decompositionStack[index];
        return _decompositionItem[index];
    }

    public bool AddItemToDecompositionSlot(ItemSO item, int index, int stack)
    {
        if (!item.IsDecomposable) return false;

        int remain = stack;
        while (remain > 0)
        {
            for (int i = 0; i < _decompositionItem.Length; i++)
            {
                if (_decompositionItem[i] == item)
                {
                    remain = DecompositionSlotTryAdd(item, i, stack);
                    if (remain <= 0) break;
                }
            }

            if (remain <= 0) break;

            for (int i = 0; i < _decompositionItem.Length; i++)
            {
                if (_decompositionItem[i] == null)
                {
                    remain = DecompositionSlotTryAdd(item, i, stack);
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

    public void MoveItemInDecompositionSlot(int startIndex, int endIndex)
    {
        // if there is no item in start cell.
        if (startIndex == -1)
        {
            Debug.Log("시작칸에 아이템 없음");
            return;
        }

        // if there is item in start cell and drag into empty cell.
        else if (_decompositionItem[startIndex] != null && endIndex != -1 && _decompositionItem[endIndex] == null)
        {
            Debug.Log("아이템 옮기기");
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

            Debug.Log("아이템 위치 바꾸기");
            ItemSO tempItem = _decompositionItem[endIndex];
            int tempStack = _decompositionStack[endIndex];

            _decompositionItem[endIndex] = _decompositionItem[startIndex];
            _decompositionStack[endIndex] = _decompositionStack[startIndex];

            _decompositionItem[startIndex] = tempItem;
            _decompositionStack[startIndex] = tempStack;
            OnDecompositionSlotUpdated?.Invoke();
        }
    }

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
            _decompositionStack[index] = item.MaxStackSize;
            amount -= (item.MaxStackSize - _decompositionStack[index]);
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

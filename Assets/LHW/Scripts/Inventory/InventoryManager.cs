using System;
using UnityEngine;
using UnityEngine.AI;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    #endregion // Singleton

    #region Init

    private void Initialize()
    {
        _inventoryItem = new ItemSO[25];
        _inventoryStack = new int[_inventoryItem.Length];

        _hotBarItem = new ItemSO[8];
    }

    #endregion

    public int InventoryCount => _inventoryItem.Length;
    public int HotBarCount => _hotBarItem.Length;

    private ItemSO[] _inventoryItem;
    private int[] _inventoryStack;

    private ItemSO[] _hotBarItem;

    public static event Action OnInventorySlotChanged;

    public ItemSO ReadFromInventory(int index, out int stack)
    {
        stack = _inventoryStack[index];
        return _inventoryItem[index];
    }

    public void MoveItemInInventory(int startIndex, int endIndex)
    {
        if (startIndex == -1)
        {
            Debug.Log("시작칸에 아이템 없음");
            return;
        }

        if (_inventoryItem[startIndex] != null && endIndex == -1)
        {
            Debug.Log("아이템 버리기");
            Vector2 position = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
            for(int i = 0; i < _inventoryStack[startIndex]; i++)
            {
                // TODO : Where to Instantiate item?
                Instantiate(_inventoryItem[startIndex].Prefab, position + Vector2.right, Quaternion.identity);                
            }
            _inventoryItem[startIndex] = null;
            _inventoryStack[startIndex] = 0;
            OnInventorySlotChanged?.Invoke();
        }

        else if (_inventoryItem[startIndex] != null && endIndex != -1 && _inventoryItem[endIndex] == null)
        {
            Debug.Log("아이템 옮기기");
            InventoryTryAdd(_inventoryItem[startIndex], endIndex, _inventoryStack[startIndex]);
            _inventoryItem[startIndex] = null;
            _inventoryStack[startIndex] = 0;
            OnInventorySlotChanged?.Invoke();
        }

        else if (_inventoryItem[startIndex] != null && _inventoryItem[endIndex] != null)
        {
            Debug.Log("아이템 위치 바꾸기");
            ItemSO tempItem = _inventoryItem[endIndex];
            int tempStack = _inventoryStack[endIndex];

            _inventoryItem[endIndex] = _inventoryItem[startIndex];
            _inventoryStack[endIndex] = _inventoryStack[startIndex];

            _inventoryItem[startIndex] = tempItem;
            _inventoryStack[startIndex] = tempStack;
            OnInventorySlotChanged?.Invoke();
        }
        Debug.Log("아무것도 안함");
    }

    /// <summary>
    /// Add item to Inventory.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool AddItemToInventory(ItemSO item, int amount = 1)
    {
        int remain = amount;
        while (remain > 0)
        {
            for (int i = 0; i < _inventoryItem.Length; i++)
            {
                if (_inventoryItem[i] == item)
                {
                    remain = InventoryTryAdd(item, i, amount);                    
                    if (remain <= 0) break;
                }
            }

            if (remain <= 0) break;

            for (int i = 0; i < _inventoryItem.Length; i++)
            {
                if (_inventoryItem[i] == null)
                {
                    remain = InventoryTryAdd(item, i, amount);
                    if (remain <= 0) break;
                }
            }

            if (remain <= 0) break;

            else
            {
                Debug.Log("inventory full"); return false;
            }
        }
        return true;
    }

    public void AddItemToHotBar(ItemSO item)
    {
        if (item.Type == ItemType.Material) return;

        for (int i = 0; i < _hotBarItem.Length; i++)
        {
            if (_hotBarItem[i] == null)
            {
                _hotBarItem[i] = item;
                break;
            }
        }
    }

    /// <summary>
    /// Use Item.
    /// </summary>
    /// <param name="index"></param>
    public void UseItem(int index)
    {
        if (_inventoryItem[index] && _inventoryItem[index].Type != ItemType.Material)
        {
            _inventoryStack[index]--;
            if( _inventoryStack[index] <= 0 )
            {
                _inventoryItem[index] = null;
                _inventoryStack[index] = 0;
            }
            OnInventorySlotChanged?.Invoke();
        }
    }

    /// <summary>
    /// Try add item.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    private int InventoryTryAdd(ItemSO item, int index, int amount)
    {
        _inventoryItem[index] = item;
        // If the stack is enough.
        if (amount <= (item.MaxStackSize - _inventoryStack[index]))
        {
            _inventoryStack[index] += amount;
            OnInventorySlotChanged?.Invoke();
            return 0;
        }
        // If the stack is not enough and has space.
        else if (item.MaxStackSize > _inventoryStack[index])
        {
            _inventoryStack[index] = item.MaxStackSize;
            amount -= (item.MaxStackSize - _inventoryStack[index]);
            OnInventorySlotChanged?.Invoke();
            return amount;
        }
        // If the stack is full.
        else
        {
            return amount;
        }
    }
}
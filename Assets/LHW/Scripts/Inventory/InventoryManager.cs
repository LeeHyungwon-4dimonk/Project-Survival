using System;
using UnityEngine;

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
        _hotBarStack = new int[_hotBarItem.Length];
    }

    #endregion

    public int InventoryCount => _inventoryItem.Length;
    public int HotBarCount => _hotBarItem.Length;

    private ItemSO[] _inventoryItem;
    private int[] _inventoryStack;

    private ItemSO[] _hotBarItem;
    private int[] _hotBarStack;

    public static event Action<int> OnInventorySlotChanged;
    public static event Action<int> OnHotbarSlotChanged;

    public ItemSO ReadFromInventory(int index, out int stack)
    {
        stack = _inventoryStack[index];
        return _inventoryItem[index];
    }

    public ItemSO ReadFromHotBar(int index, out int stack)
    {
        stack = _hotBarStack[index];
        return _hotBarItem[index];
    }

    public ItemSO GetItemFromInventory(int index, out int stack)
    {
        if (_inventoryItem[index] == null)
        {
            stack = 0;
            return null;
        }
        ItemSO item = _inventoryItem[index];
        stack = _inventoryStack[index];
        _inventoryStack[index] = 0;
        if (_inventoryStack[index] == 0)
        {
            _inventoryItem[index] = null;
        }
        OnInventorySlotChanged?.Invoke(index);
        return item;
    }

    public ItemSO GetItemFromHotBar(int index, out int stack)
    {
        if (_hotBarItem[index] == null)
        {
            stack = 0;
            return null;
        }
        ItemSO item = _hotBarItem[index];
        stack = _hotBarStack[index];
        _hotBarStack[index] = 0;
        if (_hotBarStack[index] == 0)
        {
            _hotBarItem[index] = null;
        }
        return item;
    }

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
                OnHotbarSlotChanged?.Invoke(i);
                break;
            }
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
            OnInventorySlotChanged?.Invoke(index);
            return 0;
        }
        // If the stack is not enough and has space.
        else if (item.MaxStackSize > _inventoryStack[index])
        {
            _inventoryStack[index] = item.MaxStackSize;
            amount -= (item.MaxStackSize - _inventoryStack[index]);
            OnInventorySlotChanged?.Invoke(index);
            return amount;
        }
        // If the stack is full.
        else
        {
            return amount;
        }
    }
}
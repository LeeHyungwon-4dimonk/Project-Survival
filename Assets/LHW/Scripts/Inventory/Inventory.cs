using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] protected InventorySystem _inventorySystem;

    public InventorySystem InventorySystem => _inventorySystem;

    public static Action<InventorySystem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        _inventorySystem = new InventorySystem(_inventorySize);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] protected InventorySystem _inventorySystem;

    public InventorySystem InventorySystem => _inventorySystem;

    private void Awake() => Init();

    private void Init()
    {
        _inventorySystem = new InventorySystem(_inventorySize);
    }

    [SerializeField] LHWTestItem _testItem;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            _inventorySystem.AddItem(_testItem, 2);
        }
    }
}
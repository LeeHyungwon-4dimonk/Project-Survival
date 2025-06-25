using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : Inventory
{
    [SerializeField] protected int _playerInventorySize;
    [SerializeField] protected InventorySystem _playerInventorySystem;

    public InventorySystem PlayerInventorySystem => _playerInventorySystem;

    public static Action<InventorySystem> OnPlayerBackpackDisplayRequested;

    protected override void Awake()
    {
        base.Awake();

        _playerInventorySystem = new InventorySystem(_playerInventorySize);
    }

    private void Update()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame)
            OnPlayerBackpackDisplayRequested?.Invoke(_playerInventorySystem);
    }

    public bool AddItem(LHWTestItem data, int amount)
    {
        if (_inventorySystem.AddItem(data, amount))
        {
            return true;
        }
        else if(_playerInventorySystem.AddItem(data, amount))
        {
            return true;
        }

        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : Inventory, IInteractable
{
    public string GetDescription()
    {
        return "Press E Key to Interact";
    }

    public KeyCode GetKey()
    {
        return KeyCode.E;
    }

    public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(_inventorySystem);
    }
}

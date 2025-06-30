using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DecompositionSystem : MonoBehaviour
{
    [SerializeField] private ItemSO[] _decompositionItem;
    [SerializeField] private int[] _decompositionStack;

    private void Start()
    {
        _decompositionItem = new ItemSO[25];
        _decompositionStack = new int[25];
    }

    public void ItemToInventory(int index)
    {
        InventoryManager.Instance.AddItemToInventory(_decompositionItem[index], _decompositionStack[index]);

        _decompositionItem[index] = null;
        _decompositionStack[index] = 0;
    }
}

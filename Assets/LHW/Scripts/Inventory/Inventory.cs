using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // public is used because of DataManager reference.
    // I've tryed to protect the variable in using directly,
    // but it still can be modified directly.
    // If you have idea of protecting variable, please share.
    [SerializeField] public LHWTestItem[] Items { get; private set; }

    #region Test

    // Test Scripts
    /*
    [SerializeField] LHWTestItem _potion;

    private void Awake()
    {
        Items = new LHWTestItem[5];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddItem(_potion);
        }
        if(Input.GetKey(KeyCode.S))
        {
            UseItem(0);
        }
    }
    */

    #endregion

    public bool AddItem(LHWTestItem item)
    {
        int lastIndex = -1;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                lastIndex = i;
                break;
            }
        }

        if (lastIndex == -1)
        {
            Debug.Log("Inventory is Full.");
            return false;
        }

        Items[lastIndex] = item;
        OnItemChanged?.Invoke(lastIndex);
        return true;
    }

    public bool UseItem(int index)
    {
        if(Items[index] == null) return false;
        // if item is Material, it can't used directly
        if(Items[index].Type == ItemType.Material) return false;

        if (Items[index].Type == ItemType.Usable)
        {
            Items[index].Use();
            Items[index] = null;
        }
        else if (Items[index].Type == ItemType.Equip)
        {
            // if there's Equipment item
        }
        OnItemChanged?.Invoke(index);
        return true;        
    }

    public event Action<int> OnItemChanged;
}
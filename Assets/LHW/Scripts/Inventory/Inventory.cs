using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private LHWTestItem[] _items;
    [SerializeField] private int _slots;

    public LHWTestItem[] Items => _items;
    public int slots => _slots;

    #region Test

    // Test Scripts
    
    [SerializeField] LHWTestItem _potion;

    private void Awake()
    {
        _items = new LHWTestItem[_slots];
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
    

    #endregion

    public bool AddItem(LHWTestItem item)
    {
        int lastIndex = -1;
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
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

        _items[lastIndex] = item;
        OnItemChanged?.Invoke(lastIndex);
        return true;
    }

    public bool UseItem(int index)
    {
        if(_items[index] == null) return false;
        // if item is Material, it can't used directly
        if(_items[index].Type == ItemType.Material) return false;

        if (_items[index].Type == ItemType.Usable)
        {
            _items[index].Use();
            _items[index] = null;
        }
        else if (_items[index].Type == ItemType.Equip)
        {
            // if there's Equipment item
        }
        OnItemChanged?.Invoke(index);
        return true;        
    }

    public event Action<int> OnItemChanged;
}
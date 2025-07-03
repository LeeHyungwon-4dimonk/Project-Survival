using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    [SerializeField] private ItemController _itemcon;
    [SerializeField] private bool _isLooted = false;
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    public bool IsLooted => _isLooted;

    public void OnLoot()
    {
        if (_isLooted) return;

        Debug.Log($"{_itemName} È¹µæ!");
        _isLooted = true;

        bool itemAddSuccess = InventoryManager.Instance.AddItemToInventory(_itemcon.ItemSO);
        if (itemAddSuccess)
        {
            gameObject.SetActive(false);
        }
    }
}

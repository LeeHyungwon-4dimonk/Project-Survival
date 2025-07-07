using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    [SerializeField] private ItemController _itemcon;
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    public void OnLoot()
    {
        Debug.Log($"{_itemName} È¹µæ ½Ãµµ!");

        bool itemAddSuccess = InventoryManager.Instance.AddItemToInventory(_itemcon.ItemSO);
        if (itemAddSuccess)
        {
            gameObject.SetActive(false);
        }
    }
}

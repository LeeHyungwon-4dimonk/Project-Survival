using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
public class ItemDatabaseSO : ScriptableObject
{
    public List<ItemSO> items;

    private Dictionary<int, ItemSO> _itemDictionary;

    public void Initialize()
    {
        _itemDictionary = new Dictionary<int, ItemSO>();

        foreach (var item in items)
        {
            if (!_itemDictionary.ContainsKey(item.ItemId))
            {
                _itemDictionary.Add(item.ItemId, item);
            }
            else
            {
                Debug.LogWarning($"중복된 아이템 ID 발견: {item.ItemId}");
            }
        }
    }

    public ItemSO GetItemByID(int id)
    {
        if (_itemDictionary == null) Initialize();

        _itemDictionary.TryGetValue(id, out ItemSO item);
        return item;
    }
}
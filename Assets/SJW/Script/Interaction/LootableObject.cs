using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    public enum ItemDropType
    {
        FieldDrop,
        Container,
        Terminal
    }

    [SerializeField] private ItemDropType _dropType;
    [SerializeField] private bool _isLooted = false;
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    public bool IsLooted => _isLooted;
    public ItemDropType DropType => _dropType;

    public void OnLoot()
    {
        if (_isLooted) return;

        Debug.Log($"{_itemName} 획득!");
        _isLooted = true;

        gameObject.SetActive(false); // 혹은 비활성화 등으로 대체
    }
}
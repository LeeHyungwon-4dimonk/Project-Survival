using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    public enum ItemDropType
    {
        FieldDrop,
        Container
    }

    [SerializeField] private ItemDropType _dropType;
    [SerializeField] private string _itemName = "아이템";
    [SerializeField] private bool _isLooted = false;

    public bool IsLooted => _isLooted;
    public ItemDropType DropType => _dropType;

    public void OnLoot()
    {
        if (_isLooted) return;

        Debug.Log($"{_itemName} 획득!");
        _isLooted = true;

        Destroy(gameObject); // 혹은 비활성화 등으로 대체
    }
}
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
    [SerializeField] private bool _isLooted = false;
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    public bool IsLooted => _isLooted;
    public ItemDropType DropType => _dropType;

    public void OnLoot()
    {
        if (_isLooted) return;

        Debug.Log($"{_itemName} ȹ��!");
        _isLooted = true;

        Destroy(gameObject); // Ȥ�� ��Ȱ��ȭ ������ ��ü
    }
}
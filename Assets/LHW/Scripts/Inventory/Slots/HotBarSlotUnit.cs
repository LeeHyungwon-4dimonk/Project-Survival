using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlotUnit : ItemSlotUnit
{
    public override void Awake()
    {
        _image.color = Color.clear;
        _text.text = "";
    }

    public override void UpdateUI(int index)
    {
        _item = InventoryManager.Instance.ReadFromInventory(index, out int stack);
        if (_item == null)
        {
            _image.color = Color.clear;
            _text.text = "";
        }
        else
        {
            Debug.Log(_item.Name);
            _image.color = Color.white;
            _image.sprite = _item.Icon;
            _text.text = stack > 1 ? stack.ToString() : "";
        }
    }
}

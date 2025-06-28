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

    private void OnEnable()
    {
        InventoryManager.OnHotbarSlotChanged += UpdateUI;
    }

    private void OnDisable()
    {
        InventoryManager.OnHotbarSlotChanged -= UpdateUI;
    }

    public override void UpdateUI(int index)
    {
        _item = InventoryManager.Instance.ReadFromHotBar(index, out int stack);
        if (_item == null)
        {
            _image.color = Color.clear;
            _text.text = "";
        }
        else
        {
            _image.sprite = _item.Icon;
            _text.text = stack > 1 ? stack.ToString() : "";
        }
    }
}

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
        InventoryManager.OnSlotChanged += UpdateUI;
    }

    private void OnDisable()
    {
        InventoryManager.OnSlotChanged -= UpdateUI;
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

    public override void Use(int index)
    {
        throw new System.NotImplementedException();
    }
}

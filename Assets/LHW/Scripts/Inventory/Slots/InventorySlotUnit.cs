using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// For Inventory Slot Unit.
/// </summary>
public class InventorySlotUnit : ItemSlotUnit
{
    public override void Awake()
    {
        _image.color = Color.clear;
        _text.text = "";
    }

    /// <summary>
    /// Update UI.
    /// </summary>
    /// <param name="index"></param>
    public override void UpdateUI(int index)
    {
        _item = InventoryManager.Instance.ReadFromInventory(index, out int stack);
        if(_item == null)
        {
            _image.color = Color.clear;
            _itemStack = 0;
            _text.text = "";
        }
        else
        {
            _image.color = Color.white;
            _image.sprite = _item.Icon;
            _itemStack = stack;
            _text.text = stack > 1 ? stack.ToString() : "";
        }
    }

    /// <summary>
    /// Item Use when click.(Interface)
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null)
        {
            _item.Prefab.GetComponent<ItemController>().Use();

            InventoryManager.Instance.UseItem(_index);

            UpdateUI(_index);
        }
    }
}
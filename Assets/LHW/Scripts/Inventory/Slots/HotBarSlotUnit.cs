using UnityEngine;

/// <summary>
/// For static Hotbar(quickslot) Slot Unit.
/// </summary>
public class HotBarSlotUnit : ItemSlotUnit
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
        if (_item == null)
        {
            _image.color = Color.clear;
            _text.text = "";
        }
        else
        {
            _image.color = Color.white;
            _image.sprite = _item.Icon;
            _text.text = stack > 1 ? stack.ToString() : "";
        }
    }
}

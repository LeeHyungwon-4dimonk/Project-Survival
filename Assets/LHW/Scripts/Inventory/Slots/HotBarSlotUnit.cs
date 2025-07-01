using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// For static Hotbar(quickslot) Slot Unit.
/// </summary>
public class HotBarSlotUnit : ItemSlotUnit
{
    [SerializeField] HotbarController _controller;
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
        if (index == -1) return;
        _item = InventoryManager.Instance.ReadFromInventory(index, out int stack);
        if (_item == null)
        {
            _image.color = Color.clear;
            _text.text = "";
            _controller.SetSlot(_index, -1);
        }
        else
        {
            _image.color = Color.white;
            _image.sprite = _item.Icon;
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

            InventoryManager.Instance.UseItem(_controller.GetSlot(_index));

            _controller.UpdateUISlot();
        }
    }
}

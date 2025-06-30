using UnityEngine;

public class DecompositionSlotUnit : ItemSlotUnit
{
    [SerializeField] DecompositionSystem _data;

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
        _item = _data.ReadFromDecompositionSlot(index, out int stack);
        if (_item == null)
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

    public void OnClick()
    {
        _data.ReturnItemToInventory(_index);
    }
}

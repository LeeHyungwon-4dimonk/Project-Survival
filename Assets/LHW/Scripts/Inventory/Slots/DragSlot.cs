using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Drag image Slot.
/// </summary>
public class DragSlot : MonoBehaviour
{
    static public DragSlot Instance;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        Instance = this;
        
        _image.color = Color.clear;
        _text.text = "";
    }

    /// <summary>
    /// If item is Selected, image apply.
    /// </summary>
    /// <param name="slot"></param>
    public void DragSetSlot(ItemSlotUnit slot)
    {
        if (slot.Item != null)
        {
            _image.color = Color.white;
            _image.sprite = slot.Item.Icon;
            _text.text = slot.ItemStack.ToString();
        }
    }

    /// <summary>
    /// If item is Deselected, clear image.
    /// </summary>
    public void ClearDragSlot()
    {
        _image.color = Color.clear;
        _text.text = "";
    }
}
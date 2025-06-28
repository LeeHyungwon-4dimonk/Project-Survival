using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot Instance;
    public ItemSlotUnit CurrentSlot;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        Instance = this;
        
        _image.color = Color.clear;
        _text.text = "";
    }

    public void DragSetSlot(ItemSlotUnit slot)
    {
        if (slot.Item != null)
        {
            _image.color = Color.white;
            _image.sprite = slot.Item.Icon;
            _text.text = slot.ItemStack.ToString();
        }
    }

    public void ClearDragSlot()
    {
        _image.color = Color.clear;
        _text.text = "";
    }
}

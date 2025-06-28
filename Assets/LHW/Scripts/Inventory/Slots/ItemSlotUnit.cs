using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemSlotUnit : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] protected int _index;

    [SerializeField] protected Image _image;
    [SerializeField] protected TMP_Text _text;

    protected ItemSO _item;
    public ItemSO Item => _item;

    protected int _itemStack;
    public int ItemStack => _itemStack;

    public abstract void Awake();

    public abstract void UpdateUI(int index);

    /// <summary>
    /// Item Use when click
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null)
        {
            _item.Prefab.GetComponent<ItemController>().Use();

            InventoryManager.Instance.UseItem(_index);

            UpdateUI(_index);
        }        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_item != null)
        {            
            DragSlot.Instance.DragSetSlot(this);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null) DragSlot.Instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.ClearDragSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

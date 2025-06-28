using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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

    protected static int _startDragPoint;
    protected static int _endDragPoint;

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
        _startDragPoint = -1;
        _endDragPoint = -1;
        if (_item != null)
        {
            DragSlot.Instance.DragSetSlot(this);
            DragSlot.Instance.transform.position = eventData.position;
            DragSlot.Instance.CurrentSlot = this;
            _startDragPoint = this._index;
        }

        Debug.Log($"{_startDragPoint} start");
        Debug.Log($"{_endDragPoint} end");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null) DragSlot.Instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.ClearDragSlot();

        InventoryManager.Instance.MoveItemInInventory(_startDragPoint, _endDragPoint);
    }

    public void OnDrop(PointerEventData eventData)
    {
        _endDragPoint = this._index;
    }
}

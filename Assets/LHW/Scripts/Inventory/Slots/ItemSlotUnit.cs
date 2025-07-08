using TMPro;
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

    // It should be saved as static, because each cells save different data
    protected static int _startDragPoint;
    protected static int _endDragPoint;

    // It should be saved as static, because each cells save different data
    protected static bool _startIsInventorySlot;
    protected static bool _endIsInventorySlot;

    protected static bool _startIsQuickSlot;
    protected static bool _endIsQuickSlot;

    protected static bool _startIsDecompositionSlot;
    protected static bool _endIsDecompositionSlot;

    protected static bool _startIsBoxSlot;
    protected static bool _endIsBoxSlot;


    public abstract void Awake();

    public abstract void UpdateUI(int index);

    public virtual void OnPointerClick(PointerEventData eventData) { }


    /// <summary>
    /// When Drag Start.(Interface)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPoint = -1;
        _endDragPoint = -1;
        if (_item != null)
        {
            DragSlot.Instance.DragSetSlot(this);
            DragSlot.Instance.transform.position = eventData.position;
            _startDragPoint = this._index;
        }

        _startIsInventorySlot = GetComponent<InventorySlotUnit>();
        _startIsQuickSlot = GetComponent<HotBarSlotUnit>();
        _startIsDecompositionSlot = GetComponent<DecompositionSlotUnit>();
        _startIsBoxSlot = GetComponent<BoxSlotUnit>();

        Debug.Log($"{_startDragPoint} start");
        Debug.Log($"{_endDragPoint} end");
    }

    /// <summary>
    /// While Dragging.(interface)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null) DragSlot.Instance.transform.position = eventData.position;
    }

    /// <summary>
    /// When mouse button Up.(interface)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        _endDragPoint = this._index;

        _endIsInventorySlot = GetComponent<InventorySlotUnit>();
        _endIsQuickSlot = GetComponent<HotBarSlotUnit>();
        _endIsDecompositionSlot = GetComponent<DecompositionSlotUnit>();
        _endIsBoxSlot = GetComponent<BoxSlotUnit>();
        Debug.Log(_endIsInventorySlot);
    }

    /// <summary>
    /// End Dragging.(interface)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.ClearDragSlot();

        // if start is inventory slot and end is inventory slot, or start is inventory slot and end is empty space.
        if ((_startIsInventorySlot == true && _endIsInventorySlot == true) || (_startIsInventorySlot == true && _startDragPoint != -1 && _endDragPoint == -1))
        {
            InventoryManager.Instance.MoveItemInInventory(_startDragPoint, _endDragPoint);
        }
        else if (_startIsInventorySlot == true && _endIsInventorySlot == false)
        {
            if (_endIsDecompositionSlot) InventoryManager.Instance.SendItemToDecomposition(_startDragPoint);
            else if (_endIsQuickSlot) InventoryManager.Instance.AddQuickSlotItem(_endDragPoint, _startDragPoint);
            else if (_endIsBoxSlot) InventoryManager.Instance.ReturnItemToBox(_startDragPoint);
        }
        else if (_startIsInventorySlot == false && _endIsInventorySlot == true)
        {
            if (_endIsDecompositionSlot) InventoryManager.Instance.ReturnItemFromDecomposition(_startDragPoint);
        }
        else
        {
            if (_startIsDecompositionSlot && _endIsDecompositionSlot) InventoryManager.Instance.MoveItemInDecompositionSlot(_startDragPoint, _endDragPoint);
            else if (_startIsBoxSlot && _endIsBoxSlot) InventoryManager.Instance.MoveItemInBoxSlot(_startDragPoint, _endDragPoint);
        }
    }

    public void OnDisable()
    {
        _endDragPoint = -1;
        DragSlot.Instance.ClearDragSlot();
    }
}
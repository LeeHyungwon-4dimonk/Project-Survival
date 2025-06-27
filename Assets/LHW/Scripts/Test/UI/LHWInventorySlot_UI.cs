using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LHWInventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TMP_Text _itemCount;
    [SerializeField] private InventorySlots _assignedInventorySlot;

    private Button button;

    public InventorySlots AssignedInventorySlot => _assignedInventorySlot;
    public LHWInventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<LHWInventoryDisplay>();
    }

    public void Init(InventorySlots slot)
    {
        _assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    /// <summary>
    /// Updates the inventory slot.
    /// </summary>
    /// <param name="slot"></param>
    public void UpdateUISlot(InventorySlots slot)
    {
        if(slot.Data != null)
        {
            _itemSprite.sprite = slot.Data.Icon;
            _itemSprite.color = Color.white;
            if (slot.StackSize > 1) _itemCount.text = slot.StackSize.ToString();
            else _itemCount.text = "";            
        }
        else
        {
            _itemSprite.color = Color.clear;
            _itemSprite.sprite = null;
            _itemCount.text = "";
        }
    }

    /// <summary>
    /// Updates the inventory slot.
    /// </summary>
    public void UpdateUISlot()
    {
        if(_assignedInventorySlot != null) UpdateUISlot(_assignedInventorySlot);
    }

    /// <summary>
    /// Clears the inventorySlot.
    /// </summary>
    public void ClearSlot()
    {
        _assignedInventorySlot?.ClearSlot();
        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }

    /// <summary>
    /// If the UISlot is clicked.
    /// </summary>
    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }
}
using TMPro;
using Unity.VisualScripting;
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
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if(_assignedInventorySlot != null) UpdateUISlot(_assignedInventorySlot);
    }

    public void ClearSlot()
    {
        _assignedInventorySlot?.ClearSlot();
        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }
}

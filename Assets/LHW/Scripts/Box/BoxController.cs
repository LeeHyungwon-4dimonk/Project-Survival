using UnityEngine;

public class BoxController : UIBase
{
    [SerializeField] private BoxSlotUnit[] _slots;
    [SerializeField] private BoxCollectionUnit[] _collection;
    [SerializeField] private BoxSystem _data;
    public BoxSystem Data => _data;

    private void Start()
    {
        _data.OnBoxSlotUpdated += UpdateUISlot;
        UpdateUISlot();
    }

    private void OnEnable()
    {
        _data = InventoryManager.Instance.CurrentOpenedBox;
        _data.OnBoxSlotUpdated += UpdateUISlot;
        UpdateUISlot();        
    }

    private void OnDisable()
    {
        _data.OnBoxSlotUpdated -= UpdateUISlot;
        InventoryManager.Instance.CloseBox();
    }

    private void Update()
    {
        for(int i = 0; i < _collection.Length; i++)
        {
            _collection[i].UpdateUI(i);
        }
    }

    private void UpdateUISlot()
    {
        for (int i = 0; i < _slots.Length; i++) {
            _slots[i].UpdateUI(i);
        }
    }

    public void OnClick()
    {
        _data.GetAllBoxItemIntoInventory();
    }
}

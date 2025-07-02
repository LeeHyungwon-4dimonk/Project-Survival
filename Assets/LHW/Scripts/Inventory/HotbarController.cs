using UnityEngine;

public class HotbarController : MonoBehaviour
{
    [SerializeField] HotBarSlotUnit[] _hotBarSlot;
    [SerializeField] int[] _inventoryitemSlot;

    private void Awake()
    {
        for(int i = 0; i < _inventoryitemSlot.Length; i++)
        {
            _inventoryitemSlot[i] = -1;
        }
    }

    private void OnEnable()
    {
        InventoryManager.OnInventorySlotChanged += UpdateUISlot;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventorySlotChanged -= UpdateUISlot;
    }

    public void UpdateUISlot()
    {
        for(int i = 0; i < _hotBarSlot.Length; i++)
        {
            if (_inventoryitemSlot[i] == -1) continue;
            _hotBarSlot[i].UpdateUI(_inventoryitemSlot[i]);
        }
    }

    public void SetSlot(int targetIndex, int inventoryIndex)
    {
        _inventoryitemSlot[targetIndex] = inventoryIndex;
    }

    public int GetSlot(int hotbarSlot)
    {
        return _inventoryitemSlot[hotbarSlot];
    }
}
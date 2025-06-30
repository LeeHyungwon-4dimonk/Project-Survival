using UnityEngine;

public class HotbarController : MonoBehaviour
{
    [SerializeField] HotBarSlotUnit[] _hotBarSlot;
    [SerializeField] int[] _inventoryitemSlot;

    private void OnEnable()
    {
        InventoryManager.OnInventorySlotChanged += UpdateUISlot;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventorySlotChanged -= UpdateUISlot;
    }

    private void UpdateUISlot()
    {
        for(int i = 0; i < _hotBarSlot.Length; i++)
        {
            _hotBarSlot[i].UpdateUI(_inventoryitemSlot[i]);
        }
    }
}
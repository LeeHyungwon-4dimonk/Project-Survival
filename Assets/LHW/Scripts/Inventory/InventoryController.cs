using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventorySlotUnit[] slots;
    [SerializeField] TMP_Text _weightText;

    private void OnEnable()
    {        
        InventoryManager.OnInventorySlotChanged += UpdateUISlot;
        InventoryManager.OnInventorySlotChanged += UpdateWeightText;
        UpdateUISlot();
        UpdateWeightText();
    }

    private void OnDisable()
    {
        InventoryManager.OnInventorySlotChanged -= UpdateUISlot;
        InventoryManager.OnInventorySlotChanged -= UpdateWeightText;
    }

    private void UpdateUISlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateUI(i);
        }
    }

    private void UpdateWeightText()
    {
        float weight = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item != null) weight += slots[i].Item.Weight * slots[i].ItemStack;
        }
        _weightText.text = $"kg {weight.ToString()} / 25";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventorySlotUnit[] slots;


    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        for(int i  = 0; i < slots.Length; i++)
        {
            slots[i].UpdateUI(i);
        }
    }
}

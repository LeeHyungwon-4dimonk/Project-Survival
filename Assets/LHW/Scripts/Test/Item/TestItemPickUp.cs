using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemPickUp : MonoBehaviour
{
    [SerializeField] ItemSO data;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool itemAddSuccess = InventoryManager.Instance.AddItemToInventory(data);
            if (itemAddSuccess)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

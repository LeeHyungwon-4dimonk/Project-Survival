using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJWCollectibleItem : MonoBehaviour
{
    [SerializeField] private ItemSO itemData; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemCollectionManager.Instance == null)
            {
                return;
            }

            if (itemData == null)
            {
                return;
            }

            bool collected = ItemCollectionManager.Instance.TryCollectItem(itemData);
            if (collected)
            {
                Destroy(gameObject); 
            }
        }
    }
}
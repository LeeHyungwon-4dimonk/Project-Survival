using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private CollectionSO CollectionData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemCollectionManager.Instance == null)
            {
                return;
            }

            if (CollectionData == null)
            {
                return;
            }

            bool collected = ItemCollectionManager.Instance.TryCollectItem(CollectionData);
        }
    }
}
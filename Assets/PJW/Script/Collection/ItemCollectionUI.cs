// PJWItemCollectionUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemCollectionUI : MonoBehaviour
{
    public static ItemCollectionUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Transform diaryGridRoot;
    [SerializeField] private Transform collectionGridRoot;
    [SerializeField] private GameObject entryUIPrefab;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateUI()
    {
        foreach (Transform child in diaryGridRoot)       Destroy(child.gameObject);
        foreach (Transform child in collectionGridRoot)  Destroy(child.gameObject);

        if (ItemCollectionManager.Instance == null)
        {
            Debug.LogError("ItemCollectionManager.Instance를 찾을 수 없습니다.");
            return;
        }

        var collectedIds = ItemCollectionManager.Instance.CollectedItemIds;
        var allItems     = ItemCollectionManager.Instance.GetAllItems();

        foreach (var item in allItems.OrderBy(i => i.CollectionId))
        {
            var parent = item.CollectionType == CollectionType.Diary
                         ? diaryGridRoot
                         : collectionGridRoot;

            var entryObj = Instantiate(entryUIPrefab, parent);
            var ui       = entryObj.GetComponent<ItemEntryUI>();
            if (ui != null)
            {
                bool isCollected = collectedIds.Contains(item.CollectionId);
                ui.Initialize(item, isCollected);
            }
        }

        Debug.Log($"도감 UI 업데이트: Diary({diaryGridRoot.childCount}) + CollectionItem({collectionGridRoot.childCount})");
    }
}
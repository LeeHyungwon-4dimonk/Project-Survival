// PJWItemCollectionUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemCollectionUI : MonoBehaviour
{
    public static ItemCollectionUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Transform gridRoot;
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
        foreach (Transform child in gridRoot)
            Destroy(child.gameObject);

        if (ItemCollectionManager.Instance == null)
        {
            Debug.LogError("ItemCollectionManager.Instance를 찾을 수 없습니다.");
            return;
        }

        var collectedIds = ItemCollectionManager.Instance.CollectedItemIds;
        var allItems = ItemCollectionManager.Instance.GetAllItems();

       foreach (var item in allItems.OrderBy(i => i.ItemId))
        {
            var entry = Instantiate(entryUIPrefab, gridRoot);
            var ui    = entry.GetComponent<ItemEntryUI>();
            if (ui == null) continue;

            bool collected = collectedIds.Contains(item.ItemId);
            ui.Initialize(item, collected);
        }
        Debug.Log($"도감 UI 업데이트: 총 {collectedIds.Count}개 아이템 표시");
    }
}
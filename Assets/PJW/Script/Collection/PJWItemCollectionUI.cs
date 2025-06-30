// PJWItemCollectionUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PJWItemCollectionUI : MonoBehaviour
{
    public static PJWItemCollectionUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject entryUIPrefab;
    [SerializeField] private Sprite silhouetteSprite;

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

        foreach (var item in allItems
            .Where(i => collectedIds.Contains(i.ItemId))
            .OrderBy(i => i.ItemId))
        {
            var entryObj = Instantiate(entryUIPrefab, gridRoot, false);
            var entryUI  = entryObj.GetComponent<PJWItemEntryUI>();
            if (entryUI == null)
            {
                continue;
            }

            entryUI.Initialize(item, true, silhouetteSprite);
        }

        Debug.Log($"도감 UI 업데이트: 총 {collectedIds.Count}개 아이템 표시");
    }
}
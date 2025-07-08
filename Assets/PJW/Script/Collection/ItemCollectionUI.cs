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

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in diaryGridRoot)       Destroy(child.gameObject);
        foreach (Transform child in collectionGridRoot)  Destroy(child.gameObject);

        var manager = ItemCollectionManager.Instance;
        if (manager == null) return;

        var collectedIds = manager.CollectedItemIds;
        var allItems = manager.GetAllItems()
                              .OrderBy(i => i.CollectionId);

        foreach (var item in allItems)
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
    }
}
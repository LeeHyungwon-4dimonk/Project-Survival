using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 도감(수집) 시스템을 관리하는 매니저 클래스
/// </summary>
public class ItemCollectionManager : MonoBehaviour
{
    public static ItemCollectionManager Instance { get; private set; }

    [Header("Collection Data")]  
    [SerializeField] private List<CollectionSO> allItems;
    private HashSet<int> _collectedItemIds = new(); 

    public IReadOnlyCollection<int> CollectedItemIds => _collectedItemIds;

    private const string COLLECTED_ITEMS_KEY = "CollectedItemIds";
    private const string VERSION_KEY = "SaveVersion";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        string savedVersion = PlayerPrefs.GetString(VERSION_KEY, "");
        if (savedVersion != Application.version)
        {
            PlayerPrefs.DeleteKey(COLLECTED_ITEMS_KEY);
            PlayerPrefs.SetString(VERSION_KEY, Application.version);
            PlayerPrefs.Save();
        }

        LoadCollectedItems(); 
    }

    private void Start()
    {
        if (ItemCollectionUI.Instance != null)
        {
            ItemCollectionUI.Instance.UpdateUI();
        }
    }

    /*private void Update()
    {
        // 테스트용: R 키로 초기화
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCollection();
        }
    }*/

    public bool TryCollectItem(CollectionSO item)
    {
        if (item == null)
            return false;

        if (!_collectedItemIds.Contains(item.CollectionId))
        {
            _collectedItemIds.Add(item.CollectionId);
            SaveCollectedItems();

            if (ItemCollectionUI.Instance != null)
                ItemCollectionUI.Instance.UpdateUI();

            return true;
        }
        return false;
    }

    public bool IsCollected(int itemId)
    {
        return _collectedItemIds.Contains(itemId);
    }

    public List<CollectionSO> GetAllItems() => allItems;

    private void SaveCollectedItems()
    {
        string json = JsonUtility.ToJson(new CollectedIdsWrapper { ids = _collectedItemIds.ToList() });
        PlayerPrefs.SetString(COLLECTED_ITEMS_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadCollectedItems()
    {
        if (PlayerPrefs.HasKey(COLLECTED_ITEMS_KEY))
        {
            string json = PlayerPrefs.GetString(COLLECTED_ITEMS_KEY);
            CollectedIdsWrapper wrapper = JsonUtility.FromJson<CollectedIdsWrapper>(json);
            _collectedItemIds = new HashSet<int>(wrapper.ids);
        }
        else
        {
            _collectedItemIds = new HashSet<int>();
        }
    }

    [System.Serializable]
    private class CollectedIdsWrapper
    {
        public List<int> ids;
    }

    public void ResetCollection()
    {
        _collectedItemIds.Clear();
        PlayerPrefs.DeleteKey(COLLECTED_ITEMS_KEY);
        PlayerPrefs.Save();

        if (ItemCollectionUI.Instance != null)
            ItemCollectionUI.Instance.UpdateUI();
    }
}
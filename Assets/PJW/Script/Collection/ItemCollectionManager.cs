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
    [SerializeField] private List<ItemSO> allItems;
    private HashSet<int> _collectedItemIds = new(); 

    public IReadOnlyCollection<int> CollectedItemIds => _collectedItemIds;

    private const string COLLECTED_ITEMS_KEY = "CollectedItemIds";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadCollectedItems(); 
    }

    private void Start()
    {
        if (ItemCollectionUI.Instance != null)
        {
            ItemCollectionUI.Instance.UpdateUI();
        }
    }

    /// <summary>
    /// 아이템을 수집 목록에 추가를 시도합니다.
    /// </summary>
    /// <param name="item">수집할 ItemSO 데이터</param>
    /// <returns>새롭게 수집되었으면 true, 이미 수집된 아이템이면 false를 반환합니다.</returns>
    public bool TryCollectItem(ItemSO item)
    {
        if (item == null)
        {
            Debug.LogWarning("수집하려는 아이템이 null입니다. ItemCollectionManager.TryCollectItem(null) 호출됨.");
            return false;
        }

        if (!_collectedItemIds.Contains(item.ItemId))
        {
            _collectedItemIds.Add(item.ItemId); 
            Debug.Log($"아이템 '{item.Name}' (ID: {item.ItemId}) 수집 완료!");
            SaveCollectedItems(); 

            if (ItemCollectionUI.Instance != null)
            {
                ItemCollectionUI.Instance.UpdateUI();
            }
            return true;
        }
        Debug.Log($"아이템 '{item.Name}' (ID: {item.ItemId})는 이미 수집되었습니다.");
        return false;
    }

    /// <summary>
    /// 특정 아이템 ID가 수집되었는지 확인합니다.
    /// </summary>
    /// <param name="itemId">확인할 아이템의 고유 ID</param>
    /// <returns>수집되었으면 true, 아니면 false를 반환합니다.</returns>
    public bool IsCollected(int itemId)
    {
        return _collectedItemIds.Contains(itemId);
    }

    /// <summary>
    /// 도감에 등록될 수 있는 모든 아이템 목록을 반환합니다.
    /// </summary>
    public List<ItemSO> GetAllItems() => allItems;

    /// <summary>
    /// 현재 수집된 아이템 ID 목록을 PlayerPrefs에 저장합니다.
    /// </summary>
    private void SaveCollectedItems()
    {
        string json = JsonUtility.ToJson(new CollectedIdsWrapper { ids = _collectedItemIds.ToList() });
        PlayerPrefs.SetString(COLLECTED_ITEMS_KEY, json);
        PlayerPrefs.Save(); 
        Debug.Log("수집된 아이템 목록 저장 완료!");
    }

    /// <summary>
    /// PlayerPrefs에서 수집된 아이템 ID 목록을 불러옵니다.
    /// </summary>
    private void LoadCollectedItems()
    {
        if (PlayerPrefs.HasKey(COLLECTED_ITEMS_KEY))
        {
            string json = PlayerPrefs.GetString(COLLECTED_ITEMS_KEY);
            CollectedIdsWrapper wrapper = JsonUtility.FromJson<CollectedIdsWrapper>(json);
            _collectedItemIds = new HashSet<int>(wrapper.ids);
            Debug.Log($"수집된 아이템 목록 {_collectedItemIds.Count}개 불러옴.");
        }
        else
        {
            _collectedItemIds = new HashSet<int>(); 
            Debug.Log("저장된 수집 기록이 없어 새로운 기록을 시작합니다.");
        }
    }

    [System.Serializable]
    private class CollectedIdsWrapper
    {
        public List<int> ids;
    }

    /// <summary>
    /// (테스트 용도) 모든 수집 기록을 초기화하고 UI를 갱신합니다.
    /// </summary>
    public void ResetCollection()
    {
        _collectedItemIds.Clear(); 
        PlayerPrefs.DeleteKey(COLLECTED_ITEMS_KEY); 
        PlayerPrefs.Save(); 
        Debug.Log("수집 기록이 초기화되었습니다.");
        
        if (ItemCollectionUI.Instance != null)
        {
            ItemCollectionUI.Instance.UpdateUI();
        }
    }
}
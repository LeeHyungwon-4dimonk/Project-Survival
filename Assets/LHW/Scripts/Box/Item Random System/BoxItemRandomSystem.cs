using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum BoxTier { Tier1, Tier2, Tier3 }

public class BoxItemRandomSystem : MonoBehaviour
{
    [SerializeField] BoxTier _tier;
    [SerializeField] BoxSystem _data;

    [Header("ItemSetUpA")]
    [SerializeField] BoxSetUpASO _boxSetUpASO;

    [Header("Journal - List")]
    [SerializeField] CollectionSO[] _itemB_Journal;

    [Header("Food")]
    [SerializeField] ItemSO _itemC_Food;

    [Header("Collectible - List")]
    [SerializeField] CollectionSO[] _itemD_Collection;

    private WeightedRandom<ItemSO> _weightedRandomA = new WeightedRandom<ItemSO>();
    private WeightedRandom<CollectionSO> _weightedRandomD = new WeightedRandom<CollectionSO>();

    private Queue<CollectionSO> _journalQueue = new Queue<CollectionSO>();

    Dictionary<int, float> _itemBProbableDic = new Dictionary<int, float>();
    Dictionary<int, float> _itemCProbableDic = new Dictionary<int, float>();

    private void Awake()
    {
        // Item A init
        switch (_tier)
        {
            case BoxTier.Tier1:
                ItemAInit(_boxSetUpASO, BoxTier.Tier1);
                break;
            case BoxTier.Tier2:
                ItemAInit(_boxSetUpASO, BoxTier.Tier2);
                break;
            case BoxTier.Tier3:
                ItemAInit(_boxSetUpASO, BoxTier.Tier3);
                break;
        }

        // Item B init
        ItemBInit();

        // Item C init
        ItemCInit();

        // Item D init
        ItemDInit();
    }

    private void OnEnable()
    {
        ItemAddToBox();
    }

    private void OnDisable()
    {
        _data.RemoveAllItem();
    }

    private void ItemAddToBox()
    {
        ItemASelect();
        //ItemBSelect();
        //ItemCSelect();
        ItemDSelect();
    }

    /// <summary>
    /// Select Item A.
    /// Item A is definitely collecitible, and the number of A item is 4.
    /// </summary>
    private void ItemASelect()
    {
        if (_weightedRandomA.GetList() == null) return;

        for (int i = 0; i < 4; i++)
        {
            _data.AddItemToBoxSlot(_weightedRandomA.GetRandomItem());
        }
    }

    private void ItemAInit(BoxSetUpASO boxSetUpASO, BoxTier tier)
    {
        switch (tier)
        {
            case BoxTier.Tier1:
                for(int i = 0; i < boxSetUpASO.BoxSetUpA.Count; i++)
                {
                    string itemPath = boxSetUpASO.BoxSetUpA[i].ItemName;
                    ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>($"Assets/08.ScriptableObjects/Item/{itemPath}.asset");
                    int inputNum = (int)(boxSetUpASO.BoxSetUpA[i].ProbType1 * 1000);
                    _weightedRandomA.Add(item, inputNum);
                }
                break;

            case BoxTier.Tier2:
                for (int i = 0; i < boxSetUpASO.BoxSetUpA.Count; i++)
                {
                    string itemPath = boxSetUpASO.BoxSetUpA[i].ItemName;
                    ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>($"Assets/08.ScriptableObjects/Item/{itemPath}.asset");
                    int inputNum = (int)(boxSetUpASO.BoxSetUpA[i].ProbType2 * 1000);
                    _weightedRandomA.Add(item, inputNum);
                }
                break;
            case BoxTier.Tier3:
                for (int i = 0; i < boxSetUpASO.BoxSetUpA.Count; i++)
                {
                    string itemPath = boxSetUpASO.BoxSetUpA[i].ItemName;
                    ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>($"Assets/08.ScriptableObjects/Item/{itemPath}.asset");
                    int inputNum = (int)(boxSetUpASO.BoxSetUpA[i].ProbType3 * 1000);
                    _weightedRandomA.Add(item, inputNum);
                }
                break;

            default:
                break;
        }
    }


    private void ItemBSelect()
    {
        if (_journalQueue.Count == 0) return;

        float value = _itemBProbableDic[GameManager.Instance.DayNightManager.CurrentDay];
        float randomNum = Random.Range(0.0f, value);
        if (randomNum > value) return;
        _data.AddCollection(_journalQueue.Dequeue(), 0);
    }

    /// <summary>
    /// Set Item B weightRandom. (Diary)
    /// </summary>
    private void ItemBInit()
    {
        for (int i = 0; i < _itemB_Journal.Length; i++)
        {
            _journalQueue.Enqueue(_itemB_Journal[i]);
        }

        // 현재 일지 아이템이 없고 확률 테이블을 만들 방법에 대해 고민하고 있어
        // 의사 코드로 먼저 적습니다.

        // 일차 = key, 확률 = value로 된 dictionary를 생성하고, 데이터 테이블의 정보를 SO로 만들어놓는다.

        // 딕셔너리 정보를 전부 저장(임시로 값을 전부 입력함)

        _itemBProbableDic[1] = 1;
        _itemBProbableDic[2] = 0.9f;
        _itemBProbableDic[3] = 0.8f;
        _itemBProbableDic[4] = 0.7f;
        _itemBProbableDic[5] = 0.7f;
        _itemBProbableDic[6] = 0.6f;
        _itemBProbableDic[7] = 0.5f;
        _itemBProbableDic[8] = 0.5f;
        _itemBProbableDic[9] = 0.4f;
        _itemBProbableDic[10] = 0.3f;
        _itemBProbableDic[11] = 0.3f;
        _itemBProbableDic[12] = 0.3f;
        _itemBProbableDic[13] = 0.2f;
        _itemBProbableDic[14] = 0.2f;
        _itemBProbableDic[15] = 0.2f;
    }

    private void ItemCSelect()
    {
        if (_itemCProbableDic.Count == 0) return;

        float value = _itemCProbableDic[GameManager.Instance.DayNightManager.CurrentDay];
        float randomNum = Random.Range(0.0f, value);
        if (randomNum > value) return;
        _data.AddItemToBoxSlot(_itemC_Food);
    }

    /// <summary>
    /// Set Item C weightRandom. (Food)
    /// </summary>
    private void ItemCInit()
    {
        // 현재 일지 아이템이 없고 확률 테이블을 만들 방법에 대해 고민하고 있어
        // 의사 코드로 먼저 적습니다.

        // 일차 = key, 확률 = value로 된 dictionary를 생성하고, 데이터 테이블의 정보를 SO로 만들어놓는다.


        // 딕셔너리 정보를 전부 저장 - 임시로 직접 입력해봄
        _itemCProbableDic[1] = 1;
        _itemCProbableDic[2] = 0.5f;
        _itemCProbableDic[3] = 0.1f;
        _itemCProbableDic[4] = 0.2f;
        _itemCProbableDic[5] = 0.2f;
        _itemCProbableDic[6] = 0.2f;
        _itemCProbableDic[7] = 0.1f;
        _itemCProbableDic[8] = 0.1f;
        _itemCProbableDic[9] = 0.1f;
        _itemCProbableDic[10] = 0.2f;
        _itemCProbableDic[11] = 0.2f;
        _itemCProbableDic[12] = 0.2f;
        _itemCProbableDic[13] = 0.1f;
        _itemCProbableDic[14] = 0.1f;
        _itemCProbableDic[15] = 0.1f;

    }

    /// <summary>
    /// Select Item D. (Collective)
    /// </summary>
    private void ItemDSelect()
    {
        if (_weightedRandomD.GetList() == null) return;

        float randomNum = Random.Range(0.0f, 1.0f);

        if (randomNum > 0.7f) return;


        _data.AddCollection(_weightedRandomD.GetRandomItem(), 1);
    }

    /// <summary>
    /// Set Item D weightRandom. (Collective)
    /// </summary>
    private void ItemDInit()
    {
        for (int i = 0; i < _itemD_Collection.Length; i++)
        {
            _weightedRandomD.Add(_itemD_Collection[i], 1);
        }
    }
}

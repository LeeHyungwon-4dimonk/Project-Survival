using System.Collections.Generic;
using UnityEngine;

enum BoxTier { Tier1, Tier2, Tier3 }

public class BoxItemRandomSystem : MonoBehaviour
{
    [SerializeField] BoxTier _tier;
    [SerializeField] BoxSystem _data;

    [Header("Item - Normal")]
    [SerializeField] ItemSO[] _itemA_Items1;

    [Header("Item - Rare")]
    [SerializeField] ItemSO[] _itemA_Items2;

    [Header("Item - Unique")]
    [SerializeField] ItemSO[] _itemA_Items3;

    [Header("Item - Legendary")]
    [SerializeField] ItemSO[] _itemA_Items4;

    [Header("Journal - List")]
    [SerializeField] CollectionSO[] _itemB_Journal;

    [Header("Food")]
    [SerializeField] ItemSO[] _itemC_Food;

    [Header("Collectible - List")]
    [SerializeField] CollectionSO[] _itemD_Collection;

    private WeightedRandom<ItemSO> _weightedRandomA = new WeightedRandom<ItemSO>();
    private WeightedRandom<CollectionSO> _weightedRandomD = new WeightedRandom<CollectionSO>();
    
    private Stack<CollectionSO> _journalStack = new Stack<CollectionSO>();

    private void Awake()
    {
        // Item A init
        switch (_tier)
        {
            case BoxTier.Tier1: ItemAInit(300, 145, 50, 10);
                break;
            case BoxTier.Tier2: ItemAInit(150, 200, 100, 100);
                break;
            case BoxTier.Tier3: ItemAInit(50, 125, 200, 250);
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
        ItemBSelect();
        ItemCSelect();
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

    /// <summary>
    /// Set Item A weightRandom. (Materials)
    /// </summary>
    /// <param name="normal"></param>
    /// <param name="rare"></param>
    /// <param name="unique"></param>
    /// <param name="legendary"></param>
    private void ItemAInit(int normal, int rare, int unique, int legendary)
    {
        for(int i = 0; i < _itemA_Items1.Length; i++)
        {
            _weightedRandomA.Add(_itemA_Items1[i], normal);
        }
        for(int i = 0; i < _itemA_Items2.Length; i++)
        {
            _weightedRandomA.Add(_itemA_Items2[i], rare);
        }
        for(int i = 0; i < _itemA_Items3.Length; i++)
        {
            _weightedRandomA.Add(_itemA_Items3[i], unique);
        }
        for (int i = 0; i < _itemA_Items4.Length; i++)
        {
            _weightedRandomA.Add(_itemA_Items4[i], legendary);
        }
    }

    private void ItemBSelect()
    {
        if(_journalStack.Count == 0) return;

        // if(GameManager.Instance.DayNightManager.CurrentDay == key)
        //{
            // float randomNum = Random.Range(0.0f, value);
            // if randonNum > value return;
            // 콜렉션을 추가하는 함수(_journalStack.Pop());
        //}
    }

    /// <summary>
    /// Set Item B weightRandom. (Diary)
    /// </summary>
    private void ItemBInit()
    {
        for(int i = 0; i < _itemB_Journal.Length; i++)
        {
            _journalStack.Push(_itemB_Journal[i]);
        }

        // 현재 일지 아이템이 없고 확률 테이블을 만들 방법에 대해 고민하고 있어
        // 의사 코드로 먼저 적습니다.

        // 일차 = key, 확률 = value로 된 dictionary를 생성하고, 데이터 테이블의 정보를 SO로 만들어놓는다.
        Dictionary<int, float> keyValuePairs = new Dictionary<int, float>();

        // 딕셔너리 정보를 전부 저장
    }

    private void ItemCSelect()
    {
        // if(GameManager.Instance.DayNightManager.CurrentDay == key)
        //{
        // float randomNum = Random.Range(0.0f, value);
        // if randonNum > value return;
        // _data.AddItemToBoxSlot(_itemC_Food);
        //}
    }

    /// <summary>
    /// Set Item C weightRandom. (Food)
    /// </summary>
    private void ItemCInit()
    {
        // 현재 일지 아이템이 없고 확률 테이블을 만들 방법에 대해 고민하고 있어
        // 의사 코드로 먼저 적습니다.

        // 일차 = key, 확률 = value로 된 dictionary를 생성하고, 데이터 테이블의 정보를 SO로 만들어놓는다.
        Dictionary<int, float> keyValuePairs = new Dictionary<int, float>();

        // 딕셔너리 정보를 전부 저장
    }

    /// <summary>
    /// Select Item D. (Collective)
    /// </summary>
    private void ItemDSelect()
    {
        if (_weightedRandomD.GetList() == null) return;

        float randomNum = Random.Range(0.0f, 1.0f);

        if (randomNum > 0.7f) return;

        // need to make collection item slot in box inventory.
        //_data.AddItemToBoxSlot(_weightedRandomD.GetRandomItem());
    }

    /// <summary>
    /// Set Item D weightRandom. (Collective)
    /// </summary>
    private void ItemDInit()
    {
        for(int i = 0; i < _itemD_Collection.Length; i++)
        {
            _weightedRandomD.Add(_itemD_Collection[i], 1);
        }
    }
}

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
    [SerializeField] ItemSO[] _itemB_Journal;

    [Header("Food")]
    [SerializeField] ItemSO[] _itemC_Food;

    [Header("Collectible - List")]
    [SerializeField] ItemSO[] _itemD_Collection;

    private WeightedRandom<ItemSO> _weightedRandomA = new WeightedRandom<ItemSO>();
    // private WeightedRandom<ItemSO> _weightedRandomB = new WeightedRandom<ItemSO>();
    // private WeightedRandom<ItemSO> _weightedRandomC = new WeightedRandom<ItemSO>();
    private WeightedRandom<ItemSO> _weightedRandomD = new WeightedRandom<ItemSO>();
    

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

        // Item C init

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
        // TODO : Add Item B in probability
        // TODO : Add Item C in probability
        ItemDSelect();
    }

    /// <summary>
    /// Select Item A.
    /// Item A is definitely collecitible, and the number of A item is 4.
    /// </summary>
    private void ItemASelect()
    {
        for (int i = 0; i < 4; i++)
        {
            _data.AddItemToBoxSlot(_weightedRandomA.GetRandomItem());
        }
    }

    /// <summary>
    /// Set Item A weightRandom.
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

    private void ItemBInit()
    {

    }

    private void ItemCInit()
    {

    }

    /// <summary>
    /// Select Item D.
    /// </summary>
    private void ItemDSelect()
    {
        float randomNum = Random.Range(0.0f, 1.0f);

        if (randomNum > 0.7f) return;

        _data.AddItemToBoxSlot(_weightedRandomD.GetRandomItem());
    }

    /// <summary>
    /// Set Item D weightRandom.
    /// </summary>
    private void ItemDInit()
    {
        for(int i = 0; i < _itemD_Collection.Length; i++)
        {
            _weightedRandomD.Add(_itemD_Collection[i], 10);
        }
    }
}

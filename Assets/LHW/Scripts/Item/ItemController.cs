using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;

    public ItemSO ItemSO => _itemSO;

    public void Use()
    {
        if (_itemSO.Type == ItemType.Material) return;

        else if(_itemSO.Type == ItemType.Usable)
        {
            switch(_itemSO.ItemId)
            {
                case 1009: GameObject.FindWithTag("Player").GetComponent<PlayerStats>().RecoverSaturation(_itemSO.ItemStats);
                    break;
                case 1010: GameObject.FindWithTag("Player").GetComponent<PlayerStats>().RecoverHealth(_itemSO.ItemStats);
                    break;
                default:
                    break;
            }
            Debug.Log("소모품 사용함");
        }

        else if(_itemSO.Type == ItemType.Equip)
        {
            switch(_itemSO.ItemId)
            {
                case 1011: // TODO : increase player backpack max weight
                    break;
                default:
                    break;
            }
            Debug.Log("아이템 착용");
        }        
    }
}

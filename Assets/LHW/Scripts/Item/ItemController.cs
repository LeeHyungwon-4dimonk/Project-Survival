using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;

    public void Use()
    {
        if (_itemSO.Type == ItemType.Material) return;

        else if(_itemSO.Type == ItemType.Usable)
        {
            switch(_itemSO.ItemId)
            {
                case 1009: // TODO : Recover hunger
                    break;
                case 1010: // TODO : Recover health
                    break;
            }
        }

        else if(_itemSO.Type == ItemType.Equip)
        {
            switch(_itemSO.ItemId)
            {
                case 1011: // TODO : increase player backpack max weight
                    break;
            }
        }        
    }
}

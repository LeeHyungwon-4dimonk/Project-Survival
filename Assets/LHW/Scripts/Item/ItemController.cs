using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;

    public void Use()
    {
        if (_itemSO.Type == ItemType.Material) return;

        else if(_itemSO.Type == ItemType.Usable)
        {
            // TODO : Item Use - expendables
        }

        else if(_itemSO.Type == ItemType.Equip)
        {
            // TODO : Item Equip
        }        
    }
}

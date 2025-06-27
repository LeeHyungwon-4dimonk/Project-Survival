using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    public enum ItemDropType
    {
        Precious,
        Material
    }

    [SerializeField] private ItemDropType _dropType;

    public void OnLoot()
    {
        Debug.Log($"{gameObject.name} ∑Á∆√µ ");
        Debug.Log($"{_dropType} æ∆¿Ã≈€ µÂ∑”µ ");
    }
}

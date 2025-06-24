using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ItemDropType
    {
        Precious,
        Material
    }

    [SerializeField] private ItemDropType _dropType;
    [SerializeField] private GameObject _highlightSprite;

    public void OnInteract()
    {
        Debug.Log($"{gameObject.name} 과 상호작용");
        Debug.Log($"{_dropType} 아이템 드롭됨");
    }

    public void SetHighlight(bool on)
    {
        if (_highlightSprite != null)
            _highlightSprite.SetActive(on);
    }

}

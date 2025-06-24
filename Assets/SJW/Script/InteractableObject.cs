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

    public void OnInteract()
    {
        Debug.Log($"{gameObject.name} �� ��ȣ�ۿ�");
        Debug.Log($"{_dropType} ������ ��ӵ�");
    }
}

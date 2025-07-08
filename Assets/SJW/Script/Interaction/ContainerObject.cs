using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : MonoBehaviour
{
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    [SerializeField] private GameObject _uiPanel;



    public void OnOpen()
    {
        Debug.Log($"{_itemName} 상호작용");

        if (GameManager.Instance.IsUIOpen == false)
            GameManager.Instance.InGameUIManager.ShowUI(UIType.Box);


    }
}


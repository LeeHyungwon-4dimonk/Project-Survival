using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : MonoBehaviour
{
    [SerializeField] private string _itemName;
    [SerializeField] private bool isLooted;
    public string ItemName => _itemName;

    [SerializeField] private GameObject _uiPanel; 

    public void OnOpen()
    {
        Debug.Log($"{_itemName} ¿­±â");

        if (!isLooted)
        {
            isLooted = true;

            //gameObject.SetActive(false);
        }

    }
}

using UnityEngine;

public class ContainerObject : MonoBehaviour
{
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    [SerializeField] private GameObject _uiPanel;

    [SerializeField] private BoxSystem _boxSystem;

    public void OnOpen()
    {
        Debug.Log($"{_itemName} 상호작용");
        if(!InventoryManager.Instance.BoxController.gameObject.activeSelf) InventoryManager.Instance.OpenBox(_boxSystem);
            InventoryManager.Instance.OpenBoxPanel();
    }
}


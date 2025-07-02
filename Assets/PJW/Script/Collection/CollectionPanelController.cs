using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject collectionPanel;

    private bool isOpen = false;

    private void Start()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(isOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleCollectionPanel();
        }
    }

    private void ToggleCollectionPanel()
    {
        if (collectionPanel == null) return;

        isOpen = !isOpen;
        collectionPanel.SetActive(isOpen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmingUIController : MonoBehaviour
{
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TMP_Text interactionText;

    public void Show(string message, Transform target)
    {
        interactionPanel.SetActive(true);
        interactionText.text = message;
    }

    public void Hide()
    {
        interactionPanel.SetActive(false);
    }
}


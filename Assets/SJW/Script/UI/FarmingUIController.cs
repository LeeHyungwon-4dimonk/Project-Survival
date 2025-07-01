using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FarmingUIController : MonoBehaviour
{
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private Image holdProgressBarImage; // Ãß°¡µÊ

    [SerializeField] private float panelAnchoredPosY = 611f;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FarmingUIController : MonoBehaviour
{
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private Image holdProgressBarImage; // �߰���

    [SerializeField] private float panelAnchoredPosY = 611f;

    private void OnValidate()
    {
        if (interactionPanel != null)
        {
            RectTransform rect = interactionPanel.GetComponent<RectTransform>();
            Vector2 anchored = rect.anchoredPosition;
            anchored.y = panelAnchoredPosY;
            rect.anchoredPosition = anchored;
        }
    }

    public void Show(string message, Transform target)
    {
        interactionPanel.SetActive(true);
        interactionText.text = message;
    }

    public void Hide()
    {
        interactionPanel.SetActive(false);
    }

    public void ShowHoldProgressBar(float fillAmount)
    {
        if (holdProgressBarImage != null)
        {
            holdProgressBarImage.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    public void ResetProgressBar()
    {
        if (holdProgressBarImage != null)
        {
            holdProgressBarImage.fillAmount = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ItemEntryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;

    /// <summary>
    /// 아이템 엔트리를 초기화합니다.
    /// </summary>
    /// <param name="item">데이터가 담긴 ItemSO</param>
    /// <param name="isCollected">획득 여부</param>
    public void Initialize(CollectionSO item, bool isCollected)
    {
        string silhouetteName = item.SilhouetteSprite != null ? item.SilhouetteSprite.name : "<null>";
        Debug.Log($"[ItemEntryUI] Initialize: {{ Name={item.CollectionName}, Collected={isCollected}, Silhouette={{silhouetteName}} }}");
        // 이름 및 설명 설정
        nameText.text        = isCollected ? item.CollectionName        : "???";
        descriptionText.text = isCollected ? item.CollectionDescription : "???";

        // 아이콘 교체 (획득 전에는 실루엣 사용)
        icon.sprite = isCollected
            ? item.CollectionIcon
            : item.SilhouetteSprite;

        // 툴팁 기본 숨김
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipPanel == null) return;
        tooltipPanel.transform.SetAsLastSibling();
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPanel == null) return;
        tooltipPanel.SetActive(false);
    }
}


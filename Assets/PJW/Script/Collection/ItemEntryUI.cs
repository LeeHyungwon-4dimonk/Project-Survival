using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ItemEntryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    [SerializeField] private Image backgroundImage;      // ← 새로 추가
    [SerializeField] private Image icon;
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;

    /// <summary>
    /// 아이템 엔트리를 초기화합니다.
    /// </summary>
    /// <param name="item">데이터가 담긴 CollectionSO</param>
    /// <param name="isCollected">획득 여부</param>
    public void Initialize(CollectionSO item, bool isCollected)
    {
        // 1) 배경은 항상 표시, 또는 isCollected 따라 바꿀 수 있음
        if (backgroundImage != null)
        {
            // 예: SO에 배경 스프라이트가 있다면 할당
            // backgroundImage.sprite = item.BackgroundSprite;
            // backgroundImage.color = isCollected ? Color.white : new Color(1,1,1,0.5f);
            backgroundImage.gameObject.SetActive(true);
        }

        // 2) 아이콘/텍스트 처리 (기존 코드)
        if (!isCollected)
        {
            if (icon != null)
                icon.gameObject.SetActive(false);

            nameText.text = "";
            descriptionText.text = "";
        }
        else
        {
            if (icon != null)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = item.CollectionIcon;
            }

            nameText.text = item.CollectionName;
            descriptionText.text = item.CollectionDescription;
        }

        // 3) 툴팁 비활성화
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

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
    /// <param name="item">데이터가 담긴 CollectionSO</param>
    /// <param name="isCollected">획득 여부</param>
    public void Initialize(CollectionSO item, bool isCollected)
    {
        if (!isCollected)
        {
            // 획득 전: 아이콘 비활성화, 텍스트 공백
            if (icon != null)
                icon.gameObject.SetActive(false);

            nameText.text = "";
            descriptionText.text = "";
        }
        else
        {
            // 획득 후: 아이콘 활성화 + 컬렉션 아이콘 적용, 이름/설명 출력
            if (icon != null)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = item.CollectionIcon;
            }

            nameText.text = item.CollectionName;
            descriptionText.text = item.CollectionDescription;
        }

        // 툴팁은 항상 초기 상태에서 비활성화
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

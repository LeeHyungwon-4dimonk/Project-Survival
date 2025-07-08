using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ItemEntryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    
    [Header("Sound")]
    [SerializeField] private AudioClip hoverClip;
    private AudioSource _audioSource;
    private bool _isCollected = false;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    /// <summary>
    /// 아이템 엔트리를 초기화합니다.
    /// </summary>
    /// <param name="item">데이터가 담긴 CollectionSO</param>
    /// <param name="isCollected">획득 여부</param>
    public void Initialize(CollectionSO item, bool isCollected)
    {
        _isCollected = isCollected;

        if (backgroundImage != null)
        {
            backgroundImage.gameObject.SetActive(true);
        }

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

        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipPanel == null) return;
        tooltipPanel.transform.SetAsLastSibling();
        tooltipPanel.SetActive(true);
        
        if (_isCollected && hoverClip != null)
        {
            _audioSource.PlayOneShot(hoverClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPanel == null) return;
        tooltipPanel.SetActive(false);
    }
}

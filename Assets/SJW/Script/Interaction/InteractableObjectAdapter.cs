using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableObjectAdapter : MonoBehaviour, IInteractable
{
    public enum InteractionType
    {
        FieldDrop,
        Container,
        Terminal
    }

    [SerializeField] private InteractionType _interactionType;

    private SpriteRenderer _renderer;
    private LootableObject _lootable;

    [SerializeField] private GameObject nameLabelUI;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image holdProgressBarImage;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _lootable = GetComponent<LootableObject>();

        if (nameLabelUI != null)
            nameLabelUI.SetActive(false);
    }

    public void Interact()
    {
        switch (_interactionType)
        {
            case InteractionType.FieldDrop:
                if (_lootable != null)
                {
                    _lootable.OnLoot();
                }
                else
                {
                    Debug.LogWarning($"{gameObject.name}은 FieldDrop인데 LootableObject가 없음");
                }
                break;

            case InteractionType.Container:
                Debug.Log($"{gameObject.name} Container 상호작용 실행 (추후 구현)");
                break;

            case InteractionType.Terminal:
                Debug.Log($"{gameObject.name} Terminal 상호작용 실행 (추후 구현)");
                break;

            default:
                Debug.LogWarning($"{gameObject.name} 알 수 없는 InteractionType");
                break;
        }
    }


    public string GetDescription()
    {
        return _interactionType switch
        {
            InteractionType.Container => "[Space] 열기",
            InteractionType.FieldDrop => "[Space] 줍기",
            InteractionType.Terminal => "[Space] 작동",
            _ => "[Space] 상호작용"
        };
    }

    public void SetNameLabelVisible(bool visible)
    {
        if (nameLabelUI != null)
        {
            nameLabelUI.SetActive(visible);

            if (visible && nameText != null)
            {
                if (_lootable != null && !string.IsNullOrEmpty(_lootable.ItemName))
                    nameText.text = _lootable.ItemName;
                else
                    nameText.text = gameObject.name;
            }
        }
    }


    public void SetOutline(bool on)
    {
        if (_renderer != null && _renderer.material != null)
        {
            _renderer.material.SetFloat("_OutlineEnabled", on ? 1f : 0f);
        }
    }

    public void ShowProgressBar(float ratio)
    {
        if (holdProgressBarImage == null) return;

        if (!holdProgressBarImage.gameObject.activeSelf)
            holdProgressBarImage.gameObject.SetActive(true);

        holdProgressBarImage.fillAmount = Mathf.Clamp01(ratio);
    }

    public void HideProgressBar()
    {
        if (holdProgressBarImage == null) return;

        if (holdProgressBarImage.gameObject.activeSelf)
            holdProgressBarImage.gameObject.SetActive(false);

        holdProgressBarImage.fillAmount = 0f;
    }
}

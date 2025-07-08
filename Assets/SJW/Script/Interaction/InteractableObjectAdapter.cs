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
    public InteractionType InteractionTypeValue => _interactionType;

    private SpriteRenderer _renderer;

    [SerializeField] private GameObject nameLabelUI;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image holdProgressBarImage;




    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (nameLabelUI != null)
            nameLabelUI.SetActive(false);
    }

    public void Interact()
    {
        switch (_interactionType)
        {
            case InteractionType.FieldDrop:
                var lootable = GetComponent<LootableObject>();
                if (lootable != null)
                {
                    lootable.OnLoot();
                }
                else
                {
                    Debug.LogWarning($"{gameObject.name}: FieldDrop 타입이지만 LootableObject 컴포넌트 없음");
                }
                break;

            case InteractionType.Container:
                var box = GetComponent<ContainerObject>();
                if (box != null)
                {
                    box.OnOpen();
                }
                else
                {
                    Debug.LogWarning($"{gameObject.name}: FieldDrop 타입이지만 LootableObject 컴포넌트 없음");
                }
                break;


                Debug.Log($"{gameObject.name} Container 상호작용 실행 (추후 구현)");
                break;

            case InteractionType.Terminal:
                var terminal = GetComponent<TerminalObjects>();
                if (terminal != null)
                {
                    terminal.OnPower();
                }
                else
                {
                    Debug.LogWarning($"{gameObject.name}: Terminal 타입이지만 TerminalObject 컴포넌트 없음");
                }
                break;

            default:
                Debug.LogWarning($"{gameObject.name} 알 수 없는 InteractionType");
                break;
        }
    }

    public string GetDescription()
    {
        switch (_interactionType)
        {
            case InteractionType.Container:
                return "[Space] 열기";

            case InteractionType.FieldDrop:
                return "[Space] 줍기";

            case InteractionType.Terminal:
                var terminal = GetComponent<TerminalObjects>();
                return $"{TerminalObjects.GetDisplayName(terminal.Terminal)} 조작";

            default:
                return "[Space] 상호작용";
        }
    }


    public void SetNameLabelVisible(bool visible)
    {
        if (nameLabelUI == null) return;

        nameLabelUI.SetActive(visible);

        if (!visible || nameText == null) return;

        var lootable = GetComponent<LootableObject>();
        if (lootable != null && !string.IsNullOrEmpty(lootable.ItemName))
        {
            nameText.text = lootable.ItemName;
        }
        else
        {
            nameText.text = gameObject.name;
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

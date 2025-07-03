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
        if (_lootable != null && !_lootable.IsLooted)
        {
            _lootable.OnLoot();
        }
        else
        {
            Debug.Log($"{gameObject.name}��(��) ��ȣ�ۿ� ��������� ���� �Ұ�");
        }
    }

    public string GetDescription()
    {
        if (_lootable != null && _lootable.IsLooted)
            return "";

        return _interactionType switch
        {
            InteractionType.Container => "[Space] ����",
            InteractionType.FieldDrop => "[Space] �ݱ�",
            InteractionType.Terminal => "[Space] �۵�",
            _ => "[Space] ��ȣ�ۿ�"
        };
    }

    public void SetOutline(bool on)
    {
        if (_renderer != null && _renderer.material != null)
        {
            _renderer.material.SetFloat("_OutlineEnabled", on ? 1f : 0f);
        }
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

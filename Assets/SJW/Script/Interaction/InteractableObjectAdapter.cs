using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableObjectAdapter : MonoBehaviour, IInteractable
{
    private SpriteRenderer _renderer;
    private LootableObject _lootable;

    [SerializeField] private GameObject nameLabelUI; // 오브젝트 자식에 있는 UI 프리팹
    [SerializeField] private TMP_Text nameText;

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
            _lootable.OnLoot();
        else
            Debug.Log($"{gameObject.name}은(는) 상호작용 대상이지만 루팅 불가");
    }

    public string GetDescription()
    {
        if (_lootable == null || _lootable.IsLooted)
            return "";

        return _lootable.DropType switch
        {
            LootableObject.ItemDropType.Container => "[Space] 열기",
            LootableObject.ItemDropType.FieldDrop => "[Space] 줍기",
            _ => "[Space] 상호작용"
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


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableObjectAdapter : MonoBehaviour, IInteractable
{
    private SpriteRenderer _renderer;
    private LootableObject _lootable;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _lootable = GetComponent<LootableObject>();
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
}


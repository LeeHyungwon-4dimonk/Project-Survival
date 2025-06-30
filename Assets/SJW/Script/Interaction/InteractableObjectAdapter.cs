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
            Debug.Log($"{gameObject.name}��(��) ��ȣ�ۿ� ��������� ���� �Ұ�");
    }

    public string GetDescription()
    {
        if (_lootable == null || _lootable.IsLooted)
            return "";

        return _lootable.DropType switch
        {
            LootableObject.ItemDropType.Container => "[Space] ����",
            LootableObject.ItemDropType.FieldDrop => "[Space] �ݱ�",
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
}


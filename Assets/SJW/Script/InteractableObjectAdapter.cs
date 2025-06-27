using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutlineControllable))]
public class InteractableObjectAdapter : MonoBehaviour, IInteractable
{
    private OutlineControllable _outline;
    private LootableObject _lootable;

    private void Awake()
    {
        _outline = GetComponent<OutlineControllable>();
        _lootable = GetComponent<LootableObject>();
    }

    public void Interact()
    {
        if (_lootable != null)
            _lootable.OnLoot();
        else
            Debug.Log($"{gameObject.name} �� ���� �Ұ����� ������");
    }

    public string GetDescription() => "E - Ȯ���ϱ�";
    public KeyCode GetKey() => KeyCode.E;

    public void SetOutline(bool on)
    {
        _outline?.SetOutlineEnabled(on);
    }
}

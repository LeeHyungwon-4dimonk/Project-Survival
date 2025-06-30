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
        if (_lootable != null && !_lootable.IsLooted)
            _lootable.OnLoot();
        else
            Debug.Log($"{gameObject.name}��(��) ��ȣ�ۿ� ��������� ���� �Ұ�");
    }

    public string GetDescription()
    {
        if (_lootable == null)
            return "[Space] ��ȣ�ۿ�";

        if (_lootable.IsLooted)
            return "[Space] (��� ����)"; // or �׳� null ��ȯ���� ���� ���� ä��

        return _lootable.DropType switch
        {
            LootableObject.ItemDropType.Container => "[Space] ����",
            LootableObject.ItemDropType.FieldDrop => "[Space] �ݱ�",
            _ => "[Space] ��ȣ�ۿ�"
        };
    }


    public void SetOutline(bool on)
    {
        _outline?.SetOutlineEnabled(on);
    }
}

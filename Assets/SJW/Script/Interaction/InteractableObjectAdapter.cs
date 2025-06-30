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
            Debug.Log($"{gameObject.name}은(는) 상호작용 대상이지만 루팅 불가");
    }

    public string GetDescription()
    {
        if (_lootable == null)
            return "[Space] 상호작용";

        if (_lootable.IsLooted)
            return "[Space] (비어 있음)"; // or 그냥 null 반환하지 말고 뭐라도 채워

        return _lootable.DropType switch
        {
            LootableObject.ItemDropType.Container => "[Space] 열기",
            LootableObject.ItemDropType.FieldDrop => "[Space] 줍기",
            _ => "[Space] 상호작용"
        };
    }


    public void SetOutline(bool on)
    {
        _outline?.SetOutlineEnabled(on);
    }
}

using UnityEngine;

public enum ItemType { Material, Usable, Equip }

[CreateAssetMenu (fileName = "LHWTestItem", menuName = "Data/LHWTestItem")]
public abstract class LHWTestItem : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float Weight { get; private set; }
    [field: SerializeField] public ItemType Type { get; private set; }

    // if is stackable, input number larger than 1.
    // if is unStackable, input 1.
    [field: SerializeField] public int MaxStackSize { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }

    public abstract void Use();
}
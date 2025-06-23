using UnityEngine;

[CreateAssetMenu(fileName = "LHWTestPotion", menuName = "Data/LHWTestPotion")]
public class LHWTestPotion : LHWTestItem
{
    [field: SerializeField] public int Amount;

    public override void Use()
    {
        Debug.Log($"By using Potion, heal {Amount} of Health.");
    }
}

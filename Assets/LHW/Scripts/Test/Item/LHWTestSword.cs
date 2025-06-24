using UnityEngine;

[CreateAssetMenu(fileName = "LHWTestSword", menuName = "Data/LHWTestSword")]
public class LHWTestSword : LHWTestItem
{
    [field: SerializeField] public int Attack;

    public override void Use()
    {
        Debug.Log($"By equipping this weapon, gained {Attack} of attack rate.");
    }
}
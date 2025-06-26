using UnityEngine;

/// <summary>
/// Scriptable Object that contains Recipe.
/// </summary>
[CreateAssetMenu(fileName = "Recipe", menuName = "Assets/New CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] public ItemSO[] reqItem;
    [SerializeField] public ItemSO resultItem;
    [SerializeField] public float craftingTime;
}

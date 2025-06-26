using UnityEngine;

/// <summary>
/// Scriptable Object that contains Recipe.
/// </summary>
[CreateAssetMenu(fileName = "Recipe", menuName = "Assets/New CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] public float craftingTime;
    [SerializeField] public ItemSO[] reqItem;
    [SerializeField] public ItemSO resultItem;
}
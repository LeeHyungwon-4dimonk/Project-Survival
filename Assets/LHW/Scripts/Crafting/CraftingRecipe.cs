using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object that contains Recipe.
/// </summary>
[CreateAssetMenu(fileName = "Recipe", menuName = "Assets/New CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] public int ProductID;
    [SerializeField] public float craftingTime;
    //[SerializeField] public List<ItemSO> reqItem = new();
    [SerializeField] public ItemSO resultItem;
    [SerializeField] public int ProductEnergy;
}
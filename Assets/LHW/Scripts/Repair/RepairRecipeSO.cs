using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Assets/New RepairRecipe")]
public class RepairRecipeSO : ScriptableObject
{
    [SerializeField] public int ProductId;
    [SerializeField] public Sprite Icon;
    [SerializeField] public int ProductType;
    [SerializeField] public string ProductName;
    [SerializeField] public string ProductDescription;
    [SerializeField] public int ProductEnergy;
}

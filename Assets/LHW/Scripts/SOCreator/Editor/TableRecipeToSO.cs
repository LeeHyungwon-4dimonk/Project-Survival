using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Create Recipe base on Table Data.(Will be fixed when table is created)
/// How to use : check the editor bar - Utilities - Generate Item
/// Should not be contained in the build file.
/// </summary>
public class TableRecipeToSO : MonoBehaviour
{
    [MenuItem("Utilities/Generate Recipe")]
    public static void GenerateRecipe()
    {
        var craftingRecipeTable = TableManager.Instance.GetTable<AssemblyContentTable>(TableType.AssemblyContent);
        List<AssemblyContentData> tCraftingRecipe = craftingRecipeTable.Contents;

        foreach (AssemblyContentData s in tCraftingRecipe)
        {
            CraftingRecipe craftingRecipe = ScriptableObject.CreateInstance<CraftingRecipe>();
            int.TryParse(s.ProdID, out craftingRecipe.ProductID);
            Debug.Log(craftingRecipe.ProductID);
            string resultItemDataPath = s.ItemID;
            Debug.Log(resultItemDataPath);
            craftingRecipe.ResultItem = AssetDatabase.LoadAssetAtPath<ItemSO>($"Assets/08.ScriptableObject/Item/{resultItemDataPath}.asset");
            if (craftingRecipe.ResultItem == null) Debug.Log("Data not found");
            int.TryParse(s.ProdType, out craftingRecipe.CraftingType);
            int.TryParse(s.ProdEng, out craftingRecipe.ProductEnergy);

            AssetDatabase.CreateAsset(craftingRecipe, $"Assets/08.ScriptableObjects/CraftingRecipe/{craftingRecipe.ResultItem}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
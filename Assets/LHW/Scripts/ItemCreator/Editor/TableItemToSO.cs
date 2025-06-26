using UnityEngine;
using UnityEditor;
using System.IO;
using System;

/// <summary>
/// Create Item base on Table Data.(Will be fixed when table is created)
/// How to use : check the editor bar - Utilities - Generate Item
/// Should not be contained in the build file.
/// </summary>
public class TableItemToSO
{
    private static string _itemCSVPath = "/LHW/Scripts/ItemCreator/Editor/CSV/TestItem.csv";
    [MenuItem("Utilities/Generate Item")]
    public static void GenerateItems()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _itemCSVPath);

        foreach(string s  in allLines)
        {
            string[] splitData = s.Split(",");

            if(splitData.Length != 9 )
            {
                Debug.LogWarning($"{s} could not be imported.");
                return;
            }

            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            int.TryParse(splitData[0], out item.ItemId);
            item.Name = splitData[1];
            item.Description = splitData[2];
            float.TryParse(splitData[3], out item.Weight);
            Enum.TryParse<ItemType>(splitData[4], true, out item.Type);
            int.TryParse(splitData[5], out item.MaxStackSize);
            int.TryParse(splitData[6], out item.Energy);

            string spritePath = splitData[7];
            string prefabPath = splitData[8];

            // If Item Path is selected, path will be edited.
            item.Icon = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
            item.Prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (item.Icon == null)
                Debug.LogWarning($"Sprite not found at path: {spritePath}");
            if (item.Prefab == null)
                Debug.LogWarning($"Prefab not found at path: {prefabPath}");

            AssetDatabase.CreateAsset(item, $"Assets/LHW/ItemData/{item.Name}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CSVToSO
{
    private static string _itemCSVPath = "/LHW/Scripts/ItemCreator/Editor/CSV/TestItem.csv";
    [MenuItem("Utilities/Generate Item")]
    public static void GenerateItems()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + _itemCSVPath);

        foreach(string s  in allLines)
        {
            string[] splitData = s.Split(",");

            if(splitData.Length != 6 )
            {
                Debug.Log($"{s} could not be imported.");
                return;
            }

            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Name = splitData[0];
            item.Description = splitData[1];
            float.TryParse(splitData[2], out item.Weight);
            Enum.TryParse<ItemType>(splitData[3], true, out item.Type);
            int.TryParse(splitData[4], out item.MaxStackSize);
            int.TryParse(splitData[5], out item.Energy);

            AssetDatabase.CreateAsset(item, $"Assets/LHW/ItemData/{item.Name}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}

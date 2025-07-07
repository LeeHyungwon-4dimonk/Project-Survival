using Codice.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TableBoxSetUpATableToSO : MonoBehaviour
{
    [MenuItem("Utilities/Generate BoxSetUpA")]

    public static void GenerateBoxSetUpA()
    {
        var boxSetUpATable = TableManager.Instance.GetTable<BoxSetUpTable>(TableType.BoxSetup);
        List<BoxSetUpData> boxSetUpA = boxSetUpATable.TBoxSetup;

        BoxSetUpASO boxSetUp = ScriptableObject.CreateInstance<BoxSetUpASO>();

        foreach (BoxSetUpData s in boxSetUpA)
        {
            string boxSetUpName = s.ItemID;
            ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>($"Assets/08.ScriptableObjects/Item/{boxSetUpName}.asset");
            float.TryParse(s.ProbType1, out float ProbType1);
            float.TryParse(s.ProbType2, out float ProbType2);
            float.TryParse(s.ProbType3, out float ProbType3);
            int.TryParse(s.ItemCount,out int Count);

            boxSetUp.BoxSetUpA.Add(new BoxSetup { ItemName = item, ProbType1 = 0.01f * ProbType1, ProbType2 = 0.01f * ProbType2, ProbType3 = 0.01f * ProbType3, ItemCount = Count });
        }
        AssetDatabase.CreateAsset(boxSetUp, $"Assets/08.ScriptableObjects/BoxSetUpA/BoxSetUpA.asset");

        AssetDatabase.SaveAssets();
    }
}

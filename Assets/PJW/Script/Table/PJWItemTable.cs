using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJW.Table
{
public class PJWItemTable : PJWTableBase
{
    public class ItemData
    {
        public int Item_ID;
        public string ItemName;
        public string ItemType;
        public int Item_Image;
        public string Description;
        public int Item_Energy;
        public float Item_Weight;
        public int MaximumPayload_per_Pannel;
    }

    public List<ItemData> Items = new();

    public override void Load(string csv)
    {
        var lines = csv.Split('\n');

        for (int i = 1; i < lines.Length; i++) 
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            var values = lines[i].Trim().Split(',');

            if (values.Length < 8)
            {
                Debug.LogWarning($"[PJWItemTable] 누락된 열이 있는 행 무시됨: {lines[i]}");
                continue;
            }

            var item = new ItemData
            {
                Item_ID = int.Parse(values[0]),
                ItemName = values[1],
                ItemType = values[2],
                Item_Image = int.Parse(values[3]),
                Description = values[4],
                Item_Energy = int.Parse(values[5]),
                Item_Weight = float.Parse(values[6]),
                MaximumPayload_per_Pannel = int.Parse(values[7])
            };

            Items.Add(item);
        }

        Debug.Log($"[PJWItemTable] Loaded {Items.Count} items.");
    }
}
}

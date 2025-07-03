using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ItemData
{
    public string ItemID;
    public string ItemName;
    public string ItemType;
    public string ItemTooltip;
    public string ItemEnergy;
    public string ItemWeight;
    public string MaxPayloadPerPanel;
    public string IsDecomposable;
    public string ItemStats;
    public string SpritePath;
    public string PrefabPath;
}

public class ItemTable : TableBase
{
    private const string _csvUrl =
        "https://docs.google.com/spreadsheets/d/e/2PACX-1vRV9et_Ahp7R443Ghr-ZIq1Z57pcoQASDfGF3EZL1m09eMur6X1V9HkM0FcWRbqaEGRCbuQQUnB9QHM/pub?gid=1916368705&single=true&output=csv";

    // 첫 번째 리스트 이름
    public List<ItemData> TItem { get; private set; }

    public override IEnumerator Load()
    {
        using (var www = UnityEngine.Networking.UnityWebRequest.Get(_csvUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError($"ItemTable Load Error: {www.error}");
                yield break;
            }
            TItem = Parse(www.downloadHandler.text);
            Debug.Log($"[ItemTable] Loaded {TItem.Count} items.");
        }
    }


    private List<ItemData> Parse(string csvText)
    {
        var lines = csvText
            .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var list = new List<ItemData>();

        // 첫 줄은 헤더이므로 건너뜁니다.
        for (int i = 1; i < lines.Length; i++)
        {
            var fields = ParseCsvLine(lines[i]);

            var data = new ItemData
            {
                ItemID             = fields[0],
                ItemName           = fields[1],
                ItemType           = fields[2],
                ItemTooltip        = fields[3],
                ItemEnergy         = fields[4],
                ItemWeight         = fields[5],
                MaxPayloadPerPanel = fields[6],
                IsDecomposable     = fields[7],
                ItemStats          = fields[8],
                SpritePath         = fields[9],
                PrefabPath         = fields[10]
            };

            list.Add(data);
        }

        return list;
    }

    private List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var cur = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    cur.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(cur.ToString());
                cur.Clear();
            }
            else
            {
                cur.Append(c);
            }
        }

        result.Add(cur.ToString());
        return result;
    }
}

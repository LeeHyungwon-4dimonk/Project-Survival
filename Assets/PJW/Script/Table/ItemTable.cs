// ItemTable.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ItemTable : TableBase
{
    private const string _csvUrl =
        "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ0S5NJiTAdAIQgyLnWWUgkU51n7gGnJ6VpVFgySXltxBH2e2s8Icq9kM3gxA9Wsm0xeVWjOOAq2t9H/pub?output=csv";

    public List<ItemData> Items { get; private set; }

    public override IEnumerator Load()
    {
        using (var www = UnityWebRequest.Get(_csvUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"ItemTable Load Error: {www.error}");
                yield break;
            }

            Items = Parse(www.downloadHandler.text);
            Debug.Log($"[ItemTable] Loaded {Items.Count} items.");
        }
    }

    // CSV 텍스트 전체를 파싱해 객체 리스트로 반환
    private List<ItemData> Parse(string csvText)
    {
        var lines = csvText
            .Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        var list = new List<ItemData>();

        // 첫 줄(header) 건너뛰기
        for (int i = 1; i < lines.Length; i++)
        {
            var fields = ParseCsvLine(lines[i]);
            var data = new ItemData
            {
                ItemID               = int.Parse(fields[0]),
                ItemName             = fields[1],
                ItemType             = fields[2],
                ItemSprite           = fields[3],
                ItemTooltip          = fields[4],
                ItemEnergy           = int.Parse(fields[5]),
                ItemWeight           = float.Parse(fields[6]),
                MaxPayloadPerPanel   = int.Parse(fields[7]),
                IsDecomposable       = bool.Parse(fields[8]),
                ItemStats            = int.Parse(fields[9]),
                SpritePath           = fields[10],
                PrefabPath           = fields[11]
            };
            list.Add(data);
        }
        return list;
    }

    // CSV 한 줄을 안전하게 파싱하는 헬퍼
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

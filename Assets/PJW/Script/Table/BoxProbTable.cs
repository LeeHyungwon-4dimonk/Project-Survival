using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.Networking;

public class BoxProbTable : TableBase
{
    private const string _csvUrl =
        "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ0S5NJiTAdAIQgyLnWWUgkU51n7gGnJ6VpVFgySXltxBH2e2s8Icq9kM3gxA9Wsm0xeVWjOOAq2t9H/pub?gid=1543144615&single=true&output=csv";
        

    public List<BoxProbData> Rows { get; private set; }

    public override IEnumerator Load()
    {
        using (var www = UnityWebRequest.Get(_csvUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"BoxProbTable Load Error: {www.error}");
                yield break;
            }

            Rows = Parse(www.downloadHandler.text);
            Debug.Log($"[BoxProbTable] Loaded {Rows.Count} rows.");
        }
    }

    // CSV 텍스트 전체를 파싱해 객체 리스트로 반환
    private List<BoxProbData> Parse(string csvText)
    {
        var lines = csvText
            .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var list = new List<BoxProbData>();

        for (int i = 1; i < lines.Length; i++)
        {
            var fields = ParseCsvLine(lines[i]);
            try
            {
                var data = new BoxProbData
                {
                    DayNum        = int.Parse(fields[0]),
                    BoxType1Prob  = float.Parse(fields[1]),
                    BoxType2Prob  = float.Parse(fields[2]),
                    BoxType3Prob  = float.Parse(fields[3])
                };
                list.Add(data);
            }
            catch (Exception e)
            {
                Debug.LogError($"BoxProbTable parse error on line {i+1}: {e.Message}");
            }
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

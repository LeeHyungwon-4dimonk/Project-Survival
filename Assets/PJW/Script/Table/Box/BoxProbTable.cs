using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.Networking;

public class BoxProbData
{
    public string   DayNum;
    public string BoxType1Prob;
    public string BoxType2Prob;
    public string BoxType3Prob;
}
public class BoxProbTable : TableBase
{
    private const string _csvUrl =
        "https://docs.google.com/spreadsheets/d/e/2PACX-1vRV9et_Ahp7R443Ghr-ZIq1Z57pcoQASDfGF3EZL1m09eMur6X1V9HkM0FcWRbqaEGRCbuQQUnB9QHM/pub?gid=1136769341&single=true&output=csv";

    public List<BoxProbData> TBoxProb { get; private set; }
        = new List<BoxProbData>();

    public override IEnumerator Load()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_csvUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[BoxProbTable] Load Error: {www.error}");
                yield break;
            }

            string csvText = www.downloadHandler.text;
            TBoxProb = ParseCsv(csvText);
            Debug.Log($"[BoxProbTable] Loaded {TBoxProb.Count} entries.");
        }
    }

    // CSV 텍스트 전체를 파싱해 객체 리스트로 반환 (문자열 그대로 할당)
    private List<BoxProbData> ParseCsv(string csvText)
    {
        var lines = csvText
            .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var list = new List<BoxProbData>();

        for (int i = 1; i < lines.Length; i++)
        {
            var fields = ParseCsvLine(lines[i]);
            if (fields.Count < 4)
            {
                Debug.LogWarning($"[BoxProbTable] line {i+1} 필드 개수 부족: {lines[i]}");
                continue;
            }

            var data = new BoxProbData
            {
                DayNum        = fields[0],
                BoxType1Prob  = fields[1],
                BoxType2Prob  = fields[2],
                BoxType3Prob  = fields[3]
            };
            list.Add(data);
        }

        return list;
    }

    // CSV 한 줄을 안전하게 파싱하는 헬퍼 (변경 없음)
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

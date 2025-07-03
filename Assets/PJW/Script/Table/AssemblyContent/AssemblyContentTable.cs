using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

    public class AssemblyContentTable : TableBase
    {
        private const string _csvUrl =
            "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ0S5NJiTAdAIQgyLnWWUgkU51n7gGnJ6VpVFgySXltxBH2e2s8Icq9kM3gxA9Wsm0xeVWjOOAq2t9H/pub?gid=1733571471&single=true&output=csv";

        public List<AssemblyContentData> Contents { get; private set; }

        public override IEnumerator Load()
        {
            using (var www = UnityWebRequest.Get(_csvUrl))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"AssemblyContentTable Load Error: {www.error}");
                    yield break;
                }

                Contents = Parse(www.downloadHandler.text);
                Debug.Log($"[AssemblyContentTable] Loaded {Contents.Count} entries.");
            }
        }

        private List<AssemblyContentData> Parse(string csvText)
        {
            var lines = csvText
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<AssemblyContentData>();

            for (int i = 1; i < lines.Length; i++) 
            {
                var fields = ParseCsvLine(lines[i]);
                if (fields.Count < 4)
                {
                    Debug.LogWarning($"[AssemblyContentTable] Invalid line at {i + 1}: {lines[i]}");
                    continue;
                }

                var data = new AssemblyContentData
                {
                    ProdID   = int.Parse(fields[0].Trim()),
                    ItemID   = int.Parse(fields[1].Trim()),
                    ProdType = int.Parse(fields[2].Trim()),
                    ProdEng  = int.Parse(fields[3].Trim())
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

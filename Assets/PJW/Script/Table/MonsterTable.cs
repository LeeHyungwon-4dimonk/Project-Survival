using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MonsterData
{
    public string MonID;
    public string MonName;
    public string MonSprite;
    public string MonHP;
    public string MonAttack;
    public string MonSpeed;
    public string MonAtkRange;
}

public class MonsterTable : TableBase
{
    private const string _csvUrl =
        "https://docs.google.com/spreadsheets/d/<YOUR_MONSTER_SHEET_ID>/pub?gid=<GID>&single=true&output=csv";

    public List<MonsterData> TMonster { get; private set; }
        = new List<MonsterData>();

    public override IEnumerator Load()
    {
        using (var www = UnityWebRequest.Get(_csvUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }

            var lines = www.downloadHandler.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            TMonster.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                var fields = CsvParser.ParseLine(lines[i]);
                if (fields.Count < 7)
                {
                    continue;
                }

                TMonster.Add(new MonsterData
                {
                    MonID       = fields[0],
                    MonName     = fields[1],
                    MonSprite   = fields[2],
                    MonHP       = fields[3],
                    MonAttack   = fields[4],
                    MonSpeed    = fields[5],
                    MonAtkRange = fields[6]
                });
            }
        }
    }
}

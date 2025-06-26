using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PJWTableManager : MonoBehaviour
{/*
    #region Singleton
    public static PJWTableManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _loadDataCoroutine = StartCoroutine(LoadData());
    }
    #endregion


    #region Public
    public T GetTable<T>(TableType tableType) where T : TableBase
    {

    }
    #endregion

    #region Core Load Logic

    private void RegisterAndLoadTable(TableType tableType, TableBase table, string csv)
    {
        table.Load(csv);
        _tables[tableType] = table;
    }

    private IEnumerator LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/1m8bLxgbwbyxJfMyBUrq3p4cp4dNxJoEFsbu_cah6SB8/export?format=csv&gid=1916368705";
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[PJWTableManager] CSV Load Failed: {www.error}");
            yield break;
        }
    }

    #endregion */
}

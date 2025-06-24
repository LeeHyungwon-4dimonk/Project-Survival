using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum TableType
{
    // Add TableType here
}


public class TableManager : MonoBehaviour
{
    #region Singleton

    public static TableManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //LoadAllTables();

        _loadDataCoroutine = StartCoroutine(LoadData());
    }

    #endregion // Singleton





    #region private fields

    private Dictionary<TableType, TableBase> _tables = new();
    private Coroutine _loadDataCoroutine;

    #endregion // private fields





    #region public funcs

    public T GetTable<T>(TableType tableType) where T : TableBase
    {
        if (_tables.TryGetValue(tableType, out var table)) {
            return table as T;
        }

        Debug.LogError($"Can't Find TableType : {tableType}");
        return null;
    }

    #endregion // public funcs





    #region private funcs

    private void LoadAllTables()
    {
        // Register and load all tables here
        // RegisterAndLoadTable(TableType.Example, new TExample());
    }

    private void RegisterAndLoadTable(TableType tableType, TableBase table)
    {
        _tables[tableType] = table;
        table.Load();
    }

    #endregion // private funcs

    #region test

    IEnumerator LoadData()
    {
        var url = "https://docs.google.com/spreadsheets/d/1m8bLxgbwbyxJfMyBUrq3p4cp4dNxJoEFsbu_cah6SB8/export?format=csv&gid=1916368705";
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        Debug.Log(data);
    }

    #endregion // test
}
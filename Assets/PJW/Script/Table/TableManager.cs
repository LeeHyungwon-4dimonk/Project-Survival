using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class TableManager : MonoBehaviour
{
    #region Singleton
    public static TableManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllTables();
    }
    #endregion

    private Dictionary<TableType, TableBase> _tables
        = new Dictionary<TableType, TableBase>();

    private void LoadAllTables()
    {
        RegisterAndLoadTable(TableType.Item, new ItemTable());
        RegisterAndLoadTable(TableType.BoxProb, new BoxProbTable());
        // TODO: 다른 테이블이 생기면 추가
    }

    private void RegisterAndLoadTable(TableType type, TableBase table)
    {
        _tables[type] = table;
        StartCoroutine(table.Load());
    }

    /// <summary>
    /// 로드된 테이블을 가져올 때 사용.
    /// </summary>
    public T GetTable<T>(TableType tableType) where T : TableBase
    {
        if (_tables.TryGetValue(tableType, out var table))
            return table as T;

        Debug.LogError($"Can't Find TableType : {tableType}");
        return null;
    }
}

// 해당 테이블 꺼내는 법
//var itemTable = TableManager.Instance.GetTable<ItemTable>(TableType.Item);

// 전체 리스트 꺼내기
// List<ItemData> allItems = itemTable.Items;

// 특정 아이템 꺼내기
//int targetID = 1001;
//ItemData single = allItems
//   .FirstOrDefault(x => x.ItemID == targetID);
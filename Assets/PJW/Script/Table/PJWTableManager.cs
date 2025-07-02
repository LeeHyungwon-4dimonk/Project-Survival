using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class PJWTableManager : MonoBehaviour
{
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

        LoadAllTables();
    }
    #endregion

    private Dictionary<PJWTableType, PJWTableBase> _tables
        = new Dictionary<PJWTableType, PJWTableBase>();

    private void LoadAllTables()
    {
        // Item 테이블 등록 & 로드
        RegisterAndLoadTable(PJWTableType.Item, new PJWItemTable());

        // TODO: 다른 테이블이 생기면 여기에 계속 추가
    }

    private void RegisterAndLoadTable(PJWTableType type, PJWTableBase table)
    {
        _tables[type] = table;
        StartCoroutine(table.Load());
    }

    /// <summary>
    /// 로드된 테이블을 가져올 때 사용.
    /// 예) var items = PJWTableManager.Instance.GetTable<PJWItemTable>(PJWTableType.Item).Items;
    /// </summary>
    public T GetTable<T>(PJWTableType tableType) where T : PJWTableBase
    {
        if (_tables.TryGetValue(tableType, out var table))
            return table as T;

        Debug.LogError($"Can't Find TableType : {tableType}");
        return null;
    }
}

// 해당 테이블 꺼내는 법
//var itemTable = PJWTableManager.Instance
//                 .GetTable<PJWItemTable>(PJWTableType.Item);

// 전체 리스트 꺼내기
// List<PJWItemData> allItems = itemTable.Items;
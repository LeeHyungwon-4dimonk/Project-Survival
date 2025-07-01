using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
 
public class PJWTableManager : MonoBehaviour
{
    #region Singleton
    public static PJWTableManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("PJWTableManager Awake");
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

    #region Fields
    
    // 테이블 타입별로 로드된 테이블을 저장하는 딕셔너리
    private Dictionary<TableType, TableBase> _tables = new(); 
    
    // CSV 비동기 로딩 코루틴 참조용
    private Coroutine _loadDataCoroutine;
    #endregion

    /// <summary>
    /// 등록된 테이블을 타입 기반으로 반환
    /// </summary>
    /// <typeparam name="T">반환할 테이블 타입</typeparam>
    /// <param name="tableType">찾고자 하는 테이블 타입</param>
    /// <returns>테이블 인스턴스 or null</returns>
    #region Public
    public T GetTable<T>(TableType tableType) where T : TableBase
    {
        if (_tables.TryGetValue(tableType, out var table))
            return table as T;

        Debug.LogError($"[PJWTableManager] Cannot find table: {tableType}");
        return null;
    }
    #endregion

    #region Core Load Logic


    private void LoadAllTables()
    {
        // Register and load all tables here
        // RegisterAndLoadTable(TableType.Example, new TExample());
    }
    
    private void RegisterAndLoadTable(TableType tableType, TableBase table, string csv)
    {
        // TableBase 연결 필요
        // table.Load(csv);   
        _tables[tableType] = table;
    }

    /// <summary>
    /// 구글 시트에서 CSV 데이터를 받아오는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadData()
    {
        // 내보내기 형식으로 CSV를 요청
        string url = "https://docs.google.com/spreadsheets/d/1m8bLxgbwbyxJfMyBUrq3p4cp4dNxJoEFsbu_cah6SB8/export?format=csv&gid=1916368705";
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        
        // 오류 처리
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[PJWTableManager] CSV Load Failed: {www.error}");
            yield break;
        }

        string csv = www.downloadHandler.text;
        Debug.Log($"[PJWTableManager] CSV Loaded, length = {csv.Length}");
        

        // Item Table 등록
        // RegisterAndLoadTable(TableType.Item, new ItemTable(), csv);
    }

    #endregion
}

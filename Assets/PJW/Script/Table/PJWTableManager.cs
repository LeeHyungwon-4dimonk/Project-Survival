using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PJW.Table;

public class PJWTableManager : MonoBehaviour
{
    public static PJWTableManager Instance { get; private set; }

    private Dictionary<PJWTableType, PJWTableBase> _tables = new();
    private Coroutine _loadDataCoroutine;

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

    // 테이블 등록 여부를 확인하는 헬퍼 메서드
    public bool IsTableLoaded(PJWTableType tableType)
    {
        return _tables.ContainsKey(tableType);
    }

    public T GetTable<T>(PJWTableType tableType) where T : PJWTableBase
    {
        if (_tables.TryGetValue(tableType, out var table))
            return table as T;

        Debug.LogError($"[PJWTableManager] 테이블 없음: {tableType}");
        return null;
    }

    private void RegisterAndLoadTable(PJWTableType tableType, PJWTableBase table, string csv)
    {
        try
        {
            table.Load(csv);
            Debug.Log($"[PJWTableManager] 테이블 로드 완료: {tableType}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[PJWTableManager] 테이블 로드 실패 ({tableType}): {ex.Message}");
        }

        // 예외가 나도 빈 테이블이라도 등록
        _tables[tableType] = table;
        Debug.Log($"[PJWTableManager] 테이블 등록 완료: {tableType}");
    }

    private IEnumerator LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQBZd8o_NHamDmplP5aclEm8mRG8TCod7bToxwXY8mgILdW4Ht4lf22IJWH0td8EUe8W6ec7jGrOP4g/pub?output=csv";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[PJWTableManager] CSV 다운로드 실패: {www.error}");
            yield break;
        }

        string csv = www.downloadHandler.text;
        Debug.Log($"[PJWTableManager] CSV 로드 완료, 길이: {csv.Length}");

        RegisterAndLoadTable(PJWTableType.Item, new PJWItemTable(), csv);
    }
}

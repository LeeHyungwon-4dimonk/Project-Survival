// Assets/Scripts/ItemExample.cs
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemExample : MonoBehaviour
{
    private ItemTable    _itemTable;
    private BoxProbTable _boxProbTable;

    private IEnumerator Start()
    {
        // 1) TableManager가 생성되고, 모든 테이블이 로드될 때까지 대기
        yield return new WaitUntil(() =>
            TableManager.Instance != null
            && TableManager.Instance.GetTable<ItemTable>(TableType.Item)?.TItem    != null
            && TableManager.Instance.GetTable<BoxProbTable>(TableType.BoxProb)?.TBoxProb != null
            && TableManager.Instance.TItems != null
        );

        // 2) 테이블 인스턴스 가져오기
        _itemTable    = TableManager.Instance.GetTable<ItemTable>(TableType.Item);          // 아이템 테이블 전체 가져오기
        _boxProbTable = TableManager.Instance.GetTable<BoxProbTable>(TableType.BoxProb);    // 박스 테이블 전체 가져오기

        Debug.Log("=== ItemTable.TItem에서 ItemID 뽑기 ===");
        foreach (var item in _itemTable.TItem)
        {
            Debug.Log($"ItemExample → ItemID: {item.ItemID}");
        }

        Debug.Log("=== BoxProbTable.BoxProbs에서 DayNum 뽑기 ===");
        foreach (var bp in _boxProbTable.TBoxProb)
        {
            Debug.Log($"ItemExample → DayNum: {bp.DayNum}");
        }

        Debug.Log("=== TItems에서 List<ItemData>만 뽑아서 ItemID 뽑기 ===");
        var onlyItems = TableManager.Instance.TItems.OfType<List<ItemData>>().FirstOrDefault();

        if (onlyItems != null)
        {
            foreach (var item in onlyItems)
            {
                Debug.Log($"[From TItems] ItemID: {item.ItemID}");
            }
        }
        else
        {
            Debug.LogError("ItemExample → TItems 안에 ItemData 리스트가 없습니다.");
        }

        // === 추가: TItems에서 BoxProbData 리스트만 뽑기 ===

        Debug.Log("=== TItems에서 List<BoxProbData>만 뽑아서 DayNum 뽑기 ===");
        var onlyProbs = TableManager.Instance.TItems.OfType<List<BoxProbData>>().FirstOrDefault();

        if (onlyProbs != null)
        {
            foreach (var bp in onlyProbs)
            {
                Debug.Log($"[From TItems] DayNum: {bp.DayNum}");
            }
        }
        else
        {
            Debug.LogError("ItemExample → TItems 안에 BoxProbData 리스트가 없습니다.");
        }
    }
}

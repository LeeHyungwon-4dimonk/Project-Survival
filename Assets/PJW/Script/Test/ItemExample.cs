using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExample : MonoBehaviour
{
    private ItemTable _itemTable;
    private BoxProbTable  _boxProbTable;

    IEnumerator Start()
    {
        _itemTable = TableManager.Instance.GetTable<ItemTable>(TableType.Item);

        // CSV 파싱이 비동기로 이루어지기 때문에 데이터가 모두 로드되기 전에 Items에 접근하면 null 참조 에러가 발생해서 코루틴을 넣어야 함
        yield return new WaitUntil(() =>
            _itemTable != null &&
            _itemTable.Items != null
        );

        Debug.Log($"Loaded {_itemTable.Items.Count} items.");

        foreach (var item in _itemTable.Items)
        {
            Debug.Log($"ID:{item.ItemID}, Name:{item.ItemName} ItemType:{item.ItemType}");
        }
        _boxProbTable = TableManager.Instance.GetTable<BoxProbTable>(TableType.BoxProb);

        // BoxProbTable 로드될 때까지 대기
        yield return new WaitUntil(() =>
            _boxProbTable != null &&
            _boxProbTable.Rows != null
        );

        Debug.Log($"[ItemExample] Loaded {_boxProbTable.Rows.Count} box-prob rows.");
        foreach (var row in _boxProbTable.Rows)
        {
            Debug.Log(
                $"Day {row.DayNum} → " +
                $"Type1:{row.BoxType1Prob}  " +
                $"Type2:{row.BoxType2Prob}  " +
                $"Type3:{row.BoxType3Prob}"
            );
        }
    }
}

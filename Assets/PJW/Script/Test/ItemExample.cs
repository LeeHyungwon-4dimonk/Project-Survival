using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExample : MonoBehaviour
{
    private ItemTable _itemTable;

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
    }
}

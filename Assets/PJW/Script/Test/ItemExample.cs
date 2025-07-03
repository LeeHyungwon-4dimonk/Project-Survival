using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExample : MonoBehaviour
{
    private PJWItemTable _itemTable;

    // IEnumerator를 리턴하면 Unity가 자동으로 코루틴으로 실행해 줍니다.
    IEnumerator Start()
    {
        // 1) 테이블 인스턴스 가져오기
        _itemTable = PJWTableManager.Instance
                        .GetTable<PJWItemTable>(PJWTableType.Item);

        // 2) 테이블 또는 Items 프로퍼티가 null인 동안 대기
        yield return new WaitUntil(() =>
            _itemTable != null &&
            _itemTable.Items != null
        );

        // 3) 이제 안전하게 접근 가능
        Debug.Log($"Loaded {_itemTable.Items.Count} items.");

        foreach (var item in _itemTable.Items)
        {
            Debug.Log($"ID:{item.ItemID}, Name:{item.ItemName} ItemType:{item.ItemType}");
        }
    }
}

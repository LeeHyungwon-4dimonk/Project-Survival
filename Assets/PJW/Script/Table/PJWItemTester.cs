using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PJW.Table;

public class PJWItemTester : MonoBehaviour
{
    private IEnumerator Start()
    {
        // 1) 싱글톤 준비 대기
        yield return new WaitUntil(() => PJWTableManager.Instance != null);

        // 2) Item 테이블이 로드될 때까지 대기
        yield return new WaitUntil(() => PJWTableManager.Instance.IsTableLoaded(PJWTableType.Item));

        // 3) 테이블을 한 번만 꺼내서 변수에 저장
        PJWItemTable itemTable = PJWTableManager.Instance.GetTable<PJWItemTable>(PJWTableType.Item);

        Debug.Log($"[PJWItemTester] 아이템 개수: {itemTable.Items.Count}");

        foreach (var item in itemTable.Items)
        {
            Debug.Log($"[ITEM] ID:{item.Item_ID}, 이름:{item.ItemName}, 무게:{item.Item_Weight}, 설명:{item.Description}");
        }
    }
}

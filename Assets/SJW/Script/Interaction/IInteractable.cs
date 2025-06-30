using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // 플레이어가 가까이 갔을 때 UI 에 표시할 텍스트 (예: "E 확인하기")
    string GetDescription();

    // 상호작용 실행 로직
    void Interact();
}

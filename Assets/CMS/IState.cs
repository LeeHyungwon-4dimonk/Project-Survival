using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //상태가 시작될 때 호출
    public void EnterState();
    //매 프레임마다 호출
    public void UpdateState();
    //상태가 종료될 때 호출
    public void ExitState();
}
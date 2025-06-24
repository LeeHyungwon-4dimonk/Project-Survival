using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //���°� ���۵� �� ȣ��
    public void EnterState();
    //�� �����Ӹ��� ȣ��
    public void UpdateState();
    //���°� ����� �� ȣ��
    public void ExitState();
}
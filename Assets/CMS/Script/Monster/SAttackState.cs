using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SMAttackState : MonoBehaviour, IState
{
    private Monster _monster;

    public void EnterState()
    {
        if (_monster == null) _monster = GetComponent<Monster>();
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {
    }
}

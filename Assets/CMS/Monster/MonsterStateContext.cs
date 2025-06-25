using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateContext
{
    public IState CurrentState { get; private set; }
    private readonly Monster _controller;

    public MonsterStateContext(Monster controller)
    {
        _controller = controller;
    }

    public void Transition(IState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }
        CurrentState = newState;
        CurrentState.EnterState();
    }
}

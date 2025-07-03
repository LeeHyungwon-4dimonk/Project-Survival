using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateContext : MonoBehaviour
{
    [SerializeField] private PatrolState _patrolState;

    private IState _currentState;
    public IState CurrentState => _currentState;

    private void Start()
    {
    }

    public void Initialize(Transform[] patrolPoints)
    {
        if (_patrolState != null)
        {
            _patrolState.SetPatrolPoints(patrolPoints);
        }

        Transition(_patrolState);
    }

    public void Transition(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = newState;
        _currentState.EnterState();
    }
}

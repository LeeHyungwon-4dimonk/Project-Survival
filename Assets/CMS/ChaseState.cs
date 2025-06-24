using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour, IState
{
    private Monster _monster;
    private Rigidbody2D _rb;
    private Transform _target;

    [SerializeField] private float _chaseSpeed = 3.5f;
    public void EnterState()
    {
        if (!_monster) _monster = GetComponent<Monster>();
        if (!_rb) _rb = GetComponent<Rigidbody2D>();
        if (!_target) _target = _monster.Player;
    }

    public void UpdateState()
    {
        if (_target == null) return;

        Vector2 direction = (_target.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + direction * _chaseSpeed * Time.fixedDeltaTime);
    }

    public void ExitState()
    {

    }
}


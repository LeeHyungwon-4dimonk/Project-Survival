using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState
{
    private Monster _monster;
    private Rigidbody2D _rb;
    
    [SerializeField]private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private Transform[] _patrolPoints;


    private int _currentPointIndex = 0;
    private Vector2 _lastPosition;
    private Vector2 _patrolTarget;

    public void EnterState()
    {
        if (_monster == null) _monster = GetComponent<Monster>();
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _lastPosition = _rb.position;

        if (_patrolPoints.Length > 0)
        {
            _patrolTarget = _patrolPoints[_currentPointIndex].position;
        }
    }

    public void UpdateState()
    {
        if (_patrolPoints.Length == 0) return;

        Vector2 direction = (_patrolTarget - _rb.position).normalized;
        _rb.MovePosition(_rb.position + direction * _patrolSpeed * Time.fixedDeltaTime);

        if (direction.x != 0)
            _spriteRenderer.flipX = direction.x < 0;

        if (Vector2.Distance(_rb.position, _patrolTarget) < 0.2f)
        {
            SetNextPatrolPoint();
        }
    }

    public void ExitState()
    {

    }

    private void SetNextPatrolPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        _patrolTarget = _patrolPoints[_currentPointIndex].position;
    }
}

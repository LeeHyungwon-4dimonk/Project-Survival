using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour, IState
{
    private Monster _monster;
    private Transform _target;

    [SerializeField] private float _chaseSpeed = 3.5f;

    [Header("Obstacle Avoidance")]
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _obstacleCheckRadius = 0.2f;
    [SerializeField] private float _obstacleCheckDistance = 0.5f;

    public void EnterState()
    {
        if (!_monster) _monster = GetComponent<Monster>();
        if (!_target) _target = _monster.Player;
    }

    public void UpdateState()
    {
        if (_target == null) return;

        Vector2 direction = (_target.position - transform.position).normalized;

        if (IsPathBlocked(direction))
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _target.position, _chaseSpeed * Time.deltaTime);
    }

    private bool IsPathBlocked(Vector2 direction)
    {
        Vector2 checkPoint = (Vector2)transform.position + direction.normalized * _obstacleCheckDistance;

        Collider2D hit = Physics2D.OverlapCircle(checkPoint, _obstacleCheckRadius, _obstacleLayer);

        return hit != null;
    }

    public void ExitState()
    {
    }
}

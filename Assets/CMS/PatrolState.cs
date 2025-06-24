using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState
{
    private Monster _monster;
    private Rigidbody2D _rb;

    [SerializeField] private float _patrolDistance = 3f;
    [SerializeField] private float _patrolSpeed = 2f;

    private Vector2 _leftBound;
    private Vector2 _rightBound;
    private int _direction = 1;
    public void EnterState()
    {
        if (!_monster) _monster = GetComponent<Monster>();
        if (!_rb) _rb = GetComponent<Rigidbody2D>();

        Vector2 origin = _monster.SpawnPoint;
        _leftBound = new Vector2(origin.x - _patrolDistance, origin.y);
        _rightBound = new Vector2(origin.x + _patrolDistance, origin.y);
    } 

    public void UpdateState()
    {
        Vector2 newPosition = _rb.position + Vector2.right * _direction * _patrolSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);

        if(_direction == 1 && newPosition.x >= _rightBound.x)
        {
            _direction = -1; // 방향을 왼쪽으로 변경
        }
        else if(_direction == -1 && newPosition.x <= _leftBound.x)
        {
            _direction = 1; // 방향을 오른쪽으로 변경
        }
    }

    public void ExitState()
    {
    
    }

   

}

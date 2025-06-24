using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    private Monster _monster;
    private float _attackCooldown = 1.5f;
    private float _lastAttackTime;
    public void EnterState()
    {
        if (!_monster) _monster = GetComponent<Monster>();
        _lastAttackTime = Time.time - _attackCooldown; // 바로 공격할 수 있게 초기화
    }
    public void UpdateState()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            Debug.Log("몬스터 : 공격 !");
            _lastAttackTime = Time.time;
        }
        
    }
    public void ExitState()
    {
        
    }
}

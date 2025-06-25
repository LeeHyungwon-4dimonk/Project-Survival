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
        _lastAttackTime = Time.time - _attackCooldown; // �ٷ� ������ �� �ְ� �ʱ�ȭ
    }
    public void UpdateState()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            Debug.Log("���� : ���� !");
            _lastAttackTime = Time.time;
        }
        
    }
    public void ExitState()
    {
        
    }
}

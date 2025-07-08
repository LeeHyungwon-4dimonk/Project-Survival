using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _attackCooldown = 3f;

    private Animator _animator;
    private Monster _monster;

    private float _lastAttackTime = -Mathf.Infinity;

    public void EnterState()
    {
        if (_monster == null) _monster = GetComponent<Monster>();
        if (_animator == null) _animator = GetComponent<Animator>();

        _animator.SetBool("IsAttacking", false);
    }

    public void UpdateState()
    {
        if (_monster.Player == null) return;

        float distance = Vector2.Distance(transform.position, _monster.Player.position);

        if (distance <= _monster.AttackRadius)
        {
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                _lastAttackTime = Time.time;
                _animator.SetBool("IsAttacking", true);
                StartCoroutine(PerformAttack());
            }
        }
    }

    public void ExitState()
    {
        _animator.SetBool("IsAttacking", false);
    }

    private IEnumerator PerformAttack()
    {
        Fire();
        yield return new WaitForSeconds(0.5f);
        Fire();
        _animator.SetBool("IsAttacking", false);
    }

    private void Fire()
    {
        if (_bulletPrefab == null || _firePoint == null || _monster.Player == null) return;

        Vector2 dir = (_monster.Player.position - _firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, rotation);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}

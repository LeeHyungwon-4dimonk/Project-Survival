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
    private Coroutine _attackRoutine;
    private bool _isAttacking;

    public void EnterState()
    {
        if (_monster == null) _monster = GetComponent<Monster>();
        if (_animator == null) _animator = GetComponent<Animator>();

        if (!_isAttacking)
        {
            _attackRoutine = StartCoroutine(AttackLoop());
            _isAttacking = true;
        }
    }

    public void UpdateState() { }

    public void ExitState()
    {
        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }

        _isAttacking = false;

        if (_animator != null)
            _animator.SetBool("IsAttacking", false);
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            if (_monster.Player != null)
            {
                float distance = Vector2.Distance(transform.position, _monster.Player.position);

                if (distance <= _monster.AttackRadius)
                {
                    _animator.SetBool("IsAttacking", true);

                    Fire();
                    yield return new WaitForSeconds(0.5f);
                    Fire();

                    _animator.SetBool("IsAttacking", false);
                }

                yield return new WaitForSeconds(_attackCooldown);
            }
            else
            {
                yield return null;
            }
        }
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

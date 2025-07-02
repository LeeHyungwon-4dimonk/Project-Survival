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

    public void EnterState()
    {
        Debug.Log("EnterState() called");

        if (_monster == null) _monster = GetComponent<Monster>();
        if (_animator == null) _animator = GetComponent<Animator>();

        if (_attackRoutine == null)
            _attackRoutine = StartCoroutine(AttackLoop());
    }

    public void UpdateState() { }

    public void ExitState()
    {
        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }

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
                Debug.Log("Distance to player: " + distance);

                if (distance <= _monster.AttackRadius)
                {
                    _animator.SetBool("IsAttacking", true);
                    Debug.Log("Within attack range, starting burst fire");

                    Fire();                         
                    yield return new WaitForSeconds(0.5f);
                    Fire();                     

                    _animator.SetBool("IsAttacking", false);
                    Debug.Log("Finished burst fire, waiting cooldown");

                    yield return new WaitForSeconds(_attackCooldown); 
                }
            }

            yield return null; 
        }
    }

    private void Fire()
    {
        Debug.Log("Fire() called");

        if (_bulletPrefab == null) Debug.LogWarning("_bulletPrefab is null!");
        if (_firePoint == null) Debug.LogWarning("_firePoint is null!");
        if (_monster.Player == null) Debug.LogWarning("_monster.Player is null!");

        if (_bulletPrefab == null || _firePoint == null || _monster.Player == null) return;

        Vector2 dir = (_monster.Player.position - _firePoint.position).normalized;

        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}

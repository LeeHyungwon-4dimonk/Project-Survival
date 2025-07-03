using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;
    private Animator _animator;
    private bool _isDead = false;
    public MonsterSpawner Spawner { get; set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        _animator.SetTrigger("Hit");

        Debug.Log($"���� �ǰ�: {damage}, ���� ü��: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Die");

        if (Spawner != null)
        {
            Spawner.OnMonsterDied();
        }

        Destroy(gameObject, 2.0f);
    }
}
